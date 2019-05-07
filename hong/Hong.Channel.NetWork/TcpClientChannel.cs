using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using Hong.Common.Systemer;
using Hong.Channel.Base;
using System.Windows.Forms;

namespace Hong.Channel.NetWork
{
	public class TcpClientChannel : ChannelBase, System.IDisposable
	{
		private TcpClient _tcpClient;
		private TcpClientConfig _config;
		private object _lockConnected;
		private Thread _threadReceiveData;
		private ManualResetEvent _shutdownEvent;
		private byte[] _recData;
		private int _recLen;

		public TcpClientChannel()
		{
			_config = new TcpClientConfig();

			_lockConnected = new object();

			_shutdownEvent = new ManualResetEvent(false);
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
						OpenTcpClient();
					}
					else
					{
						CloseTcpClient(TcpCloseReason.External);
					}
				}
			}
		}

		private void OpenTcpClient()
		{
			lock (_lockConnected)
			{
				if (_connected)
				{
					return;
				}
				try
				{
					_recData = new byte[_tcpClient.ReceiveBufferSize];
					_recLen = 0;

					_tcpClient = new TcpClient();
					_tcpClient.Connect(_config.IPPointRemote);
					//_tcpClient.SendBufferSize = 1024000;

					_shutdownEvent.Reset();
					_threadReceiveData = new Thread(new ThreadStart(ThreadingReceiveData));
					_threadReceiveData.IsBackground = true;
					_threadReceiveData.Start();

					_connected = true;
					SystemMessager.OutInfoGeneral(String.Format("Open TCPClient [{0} : {1:G}] Success", _config.IPPointRemote.Address.ToString(), _config.IPPointRemote.Port));
					ConnectChangedEvents(false, true);
				}
				catch (Exception exc)
				{
					SystemMessager.OutInfoException(String.Format("Open TCPClient [{0} : {1:G}] - [ {2} ]", _config.IPPointRemote.Address.ToString(), _config.IPPointRemote.Port, exc.Message));
				}
			}
		}

		private enum TcpCloseReason
		{
			RemoteDisconnection,
			WriteError,
			Dispose,
			External
		}

		private void CloseTcpClient(TcpCloseReason closeReason)
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
					_shutdownEvent.Set();

					//停止接收线程
					if (closeReason != TcpCloseReason.RemoteDisconnection && _threadReceiveData != null)
					{
						//先关闭读写
						if (_tcpClient != null)
						{
							_tcpClient.Client.Shutdown(SocketShutdown.Both);
						}
						//停止接收线程
						if (!_threadReceiveData.Join(3000))
							_threadReceiveData.Abort();
						_threadReceiveData = null;
					}

					//关闭并释放TcpClient
					if (_tcpClient != null)
					{
						_tcpClient.Close();
						_tcpClient = null;
					}

					_connected = false;
					SystemMessager.OutInfoGeneral(String.Format("Close TCPClient [{0} : {1:G}] Success", _config.IPPointRemote.Address.ToString(), _config.IPPointRemote.Port));
					//关闭事件的通知
					ConnectChangedEvents(true, false);
				}
				catch (Exception exc)
				{
					SystemMessager.OutInfoException(String.Format("Close TCPClient [{0} : {1:G}]  - [ {2} ]", _config.IPPointRemote.Address.ToString(), _config.IPPointRemote.Port, exc.Message));
				}
			}
		}

		private void ThreadingReceiveData()
		{
			//如果未接收到停止信号,则继续循环接收数据
			while (! _shutdownEvent.WaitOne(0))
			{
				try
				{
					//接收数据
					NetworkStream stream = _tcpClient.GetStream();
					// Check to see if this NetworkStream is readable.
					if (stream.CanRead)
					{
						do
						{
							_recLen = stream.Read(_recData, 0, _recData.Length);
							//byte[] buf = new byte[len];
							//Array.Copy(_recData, 0, buf, 0, len);
							if (_recLen > 0)
							{
								ReceivedDataEvents(_recData, 0, _recLen);
							}
							else if (! _shutdownEvent.WaitOne(0))
							{
								CloseTcpClient(TcpCloseReason.RemoteDisconnection);
								SystemMessager.OutInfoError("TCPClient Received 0 Bytes, So Close It");
								return;
							}
						}
						while (stream.DataAvailable);
					}
					else
					{
						//Console.WriteLine("Sorry.  You cannot read from this NetworkStream.");
					}
				}
				catch (Exception exc)
				{
					CloseTcpClient(TcpCloseReason.RemoteDisconnection);
					SystemMessager.OutInfoException(exc.Message);
					return;
				}
			}
		}

		public override OutBufferType BufferTypeReceived()
		{
			return OutBufferType.TcpClientReceived;
		}

		public override OutBufferType BufferTypeSent()
		{
			return OutBufferType.TcpClientSent;
		}

		protected override int WriteDataImpl(byte[] buf, int index, int count)
		{
			//发送数据
			try
			{
				NetworkStream stream = _tcpClient.GetStream();
				if (! stream.CanWrite)
				{
					return -1;
				}

				lock (_lockConnected)
				{
					if (! _connected)
					{
						return -1;
					}
				}

				stream.Write(buf, index, count);
				stream.Flush();
				return count;
			}
			catch (Exception exc)
			{
				CloseTcpClient(TcpCloseReason.WriteError);
				SystemMessager.OutInfoException(exc.Message);
				return -1;
			}
		}

		/*

					private WaitCallback callReceiveEvents;

						callReceiveEvents = new WaitCallback(this.CallReceiveEvents);
			
				 * internal event SerialDataReceivedEventHandler DataReceived;

				 * 
					private void CallReceiveEvents(object state)
					{
						int num = (int)state;
						SerialStream target = (SerialStream)this.streamWeakReference.Target;
						if (target != null)
						{
							if (target.DataReceived != null)
							{
								if ((num & 1) != 0)
								{
									target.DataReceived(target, new SerialDataReceivedEventArgs(SerialData.Chars));
								}
								if ((num & 2) != 0)
								{
									target.DataReceived(target, new SerialDataReceivedEventArgs(SerialData.Eof));
								}
							}
							target = null;
						}
					}
					*/
		//ThreadPool.QueueUserWorkItem(this.callReceiveEvents, nativeEvents);

		#region 释放非托管资源
		private bool _isDisposed = false;

		~TcpClientChannel()
		{
			Dispose();
		}

		public void Dispose()
		{
			Dispose(true);
		}

		protected virtual void Dispose(bool disposing)
		{
			if ((! _isDisposed) && disposing)
			{
				CloseTcpClient(TcpCloseReason.Dispose);
				_isDisposed = true;
				GC.SuppressFinalize(this);
			}
		}
		#endregion

		protected override Type ChannelConfigViewType()
		{
			return typeof(TcpClientConfigWin);
		}

		protected override ChannelConfig ChannelConfig()
		{
			return _config;
		}
	}
}
