using System;
using System.Collections.Generic;
using System.Text;
using Hong.Channel.Base;
using System.Net.Sockets;
using System.Collections;
using System.Threading;
using Hong.Common.Systemer;
using System.Net;

namespace Hong.Channel.NetWork
{
	public class TcpListenerChannel : ChannelBase, IDisposable
	{
		private TcpListener _tcpListener;
		private TcpListenerConfig _config;
		private object _lockConnected;
		private object _lockTcpListenerClients;
		private Thread _listenThread;
		private ManualResetEvent _stopEvent;
		private Hashtable _tcpListenerClients;

		public TcpListenerChannel()
		{
			_config = new TcpListenerConfig();
			_tcpListenerClients = new Hashtable();
			_lockConnected = new object();
			_lockTcpListenerClients = new object();
			_stopEvent = new ManualResetEvent(false);
		}

	    private void StartTcpListener()
        {
			lock(_lockConnected)
			{
				if (_connected)
				{
					return;
				}
				try
				{
					_tcpListener = new TcpListener(IPAddress.Any, _config.PortListen.Value);
					_tcpListener.Start();

					//侦听客户端连接
					_stopEvent.Reset();
					_listenThread = new Thread(new ThreadStart(ListenClient));
					_listenThread.IsBackground = true;
					_listenThread.Start();
					
					_connected = true;
					SystemMessager.OutInfoGeneral(String.Format("Open TCPListener [{0:G}] Success", _config.PortListen.Value));
					ConnectChangedEvents(false, true);
				}
				catch (Exception exc)
				{
					SystemMessager.OutInfoException(String.Format("Open TCPListener [{0:G}] - [ {1} ]", _config.PortListen.Value, exc.Message));
				}
			}
        }

        private void StopTcpListener()
        {
			lock (_lockConnected)
			{
				if (! _connected)
				{
					return;
				}
				try
				{
					//发送停止信号
					_stopEvent.Set();

					//停止接收线程
					lock (_lockTcpListenerClients)
					{
						foreach (TcpListenerClient tcpListenerClient in _tcpListenerClients.Values)
						{
							if (tcpListenerClient != null)
							{
								tcpListenerClient.Dispose();
							}
						}
						_tcpListenerClients.Clear();

						if (_tcpListener != null)
							_tcpListener.Stop();
						_tcpListener = null;
					}

					//停止接收线程
					if (!_listenThread.Join(3000))
						_listenThread.Abort();
					_listenThread = null;

					_connected = false;
					SystemMessager.OutInfoGeneral(String.Format("Close TcpListener [{0:G}] Success", _config.PortListen.Value));
					//关闭事件的通知
					ConnectChangedEvents(true, false);
				}
				catch(Exception exc)
				{
					SystemMessager.OutInfoException(String.Format("Close TcpListener [{0:G}]  - [ {1} ]", _config.PortListen.Value, exc.Message));
				}
			}
        }

		private void ListenClient()
        {
			while (! _stopEvent.WaitOne(0))
            {
                if (_tcpListener.Pending())
                {
					TcpClient tcpClient = _tcpListener.AcceptTcpClient();
                    IPEndPoint remote = (IPEndPoint)tcpClient.Client.RemoteEndPoint;
					TcpListenerClient tcpListenerClient = new TcpListenerClient(this, tcpClient);
					if (_stopEvent.WaitOne(0))
					{
						tcpListenerClient.ReceivedDataing = null;
						tcpListenerClient.TCPListenerClientErroring = null;
						tcpListenerClient.Dispose();
						tcpListenerClient = null;
						return;
					}
					lock (_lockTcpListenerClients)
                    {
						if (_tcpListenerClients[tcpListenerClient.AddressPortRemote] != null)
                        {
							TcpListenerClient oldTcpListenerClient = (TcpListenerClient)_tcpListenerClients[tcpListenerClient.AddressPortRemote];
							oldTcpListenerClient.ReceivedDataing = null;
							oldTcpListenerClient.TCPListenerClientErroring = null;
							oldTcpListenerClient.Dispose();
                            oldTcpListenerClient = null;
                        }
						_tcpListenerClients.Remove(tcpListenerClient.AddressPortRemote);
						if (tcpListenerClient.StartTcpClient())
						{
							_tcpListenerClients.Add(tcpListenerClient.AddressPortRemote, tcpListenerClient);
							tcpListenerClient.ReceivedDataing = ClientsReceiveData;
							tcpListenerClient.TCPListenerClientErroring = ClientsError;
						}
						else
						{
							//string addressPortRemote = tcpListenerClient.AddressPortRemote;
							tcpListenerClient.Dispose();
							tcpListenerClient = null;
							//SystemMessager.OutInfoError("");
						}
                    }
                }
                else
                {
                    Thread.Sleep(100);
                }
            }
        }

		private int ClientsReceiveData(TcpListenerClient tcpListenerClient, byte[] buf, int index, int count)
		{
			return ReceivedDataEvents(buf, index, count);
		}

		private void ClientsError(TcpListenerClient tcpListenerClient, TcpListenerClient.TcpListenerClientErrorType errorType)
		{
			lock (_lockTcpListenerClients)
			{
				_tcpListenerClients.Remove(tcpListenerClient.AddressPortRemote);
				tcpListenerClient.ReceivedDataing = null;
				tcpListenerClient.TCPListenerClientErroring = null;
				if (errorType == TcpListenerClient.TcpListenerClientErrorType.ReceiveDataEmpty)
				{
					tcpListenerClient.StopTcpClient(true);
				}
				else
				{
					tcpListenerClient.StopTcpClient(false);
				}
				tcpListenerClient = null; 
			}
		}

		public override OutBufferType BufferTypeReceived()
		{
			return OutBufferType.TcpServerReceived;
		}

		public override OutBufferType BufferTypeSent()
		{
			return OutBufferType.TcpServerSent;
		}

		private bool _connected;
		public override bool Connected
		{
			get
			{
				return _connected;
			}
			set
			{
				if (value != _connected)
				{
					if (value)
					{
						StartTcpListener();
					}
					else
					{
						StopTcpListener();
					}
				}
			}
		}

		protected override int WriteDataImpl(byte[] buf, int index, int count)
		{
			int count3 = 0;
			foreach (TcpListenerClient tcpListenerClient in _tcpListenerClients.Values)
			{
				count3 = count3 + tcpListenerClient.WriteDataImpl(buf, index, count);
			}
			return count3;
		}

		#region IDisposable 成员

		public void Dispose()
		{
			throw new NotImplementedException();
		}

		#endregion

		protected override Type ChannelConfigViewType()
		{
			return typeof(TcpListenerConfigWin);
		}

        protected override ChannelConfig ChannelConfig()
		{
			return _config;
		}
	}
}
