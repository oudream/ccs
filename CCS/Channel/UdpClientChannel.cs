using System;
using System.Collections.Generic;
using System.Text;
using Hong.Channel.Base;
using System.Net.Sockets;
using System.Net;
using Hong.Common.Systemer;
using System.Threading;
using System.Collections;
using System.Windows.Forms;

namespace Hong.Channel.NetWork
{
	public class UdpClientChannel : ChannelBase, IDisposable
	{
		private UdpClient _udpClient;
		private object _lockConnected;
		private Thread _threadReceiveData;
		private IPEndPoint _recIPEndPoint;
		private ManualResetEvent _shutdownEvent;
		private Hashtable _recIPEndPoints; 

		public UdpClientChannel()
		{
			_lockConnected = new object();

			_config = new UdpClientConfig();
			_config.Load();

			_shutdownEvent = new ManualResetEvent(false);

			_recIPEndPoints = new Hashtable();
		}

		private UdpClientConfig _config;
		public UdpClientConfig Config
		{
			get
			{
				return _config;
			}
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
						OpenUdpClient();
					}
					else
					{
						CloseUdpClient(TcpCloseReason.External);
					}
				}
			}
		}

		private void OpenUdpClient()
		{
			lock (_lockConnected)
			{
				if (_connected)
				{
					return;
				}
				try
				{
					_udpClient = new UdpClient(_config.PortListen.Value);
					//_udpClient.Connect(_iPPointRemote);

					_shutdownEvent.Reset();
					_threadReceiveData = new Thread(new ThreadStart(ThreadingReceiveData));
					_threadReceiveData.IsBackground = true;
					_threadReceiveData.Start();

					_connected = true;
					SystemMessager.OutInfoGeneral(String.Format("Open TCPClient [{0}] Success", _config.IPPointWrite.ToString()));
					ConnectChangedEvents(false, true);
				}
				catch (Exception exc)
				{
					SystemMessager.OutInfoException(String.Format("Open TCPClient [{0}] - [ {1} ]", _config.IPPointWrite.ToString(), exc.Message));
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

		private void CloseUdpClient(TcpCloseReason closeReason)
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
						if (_udpClient != null)
						{
							_udpClient.Client.Shutdown(SocketShutdown.Both);
						}
						//停止接收线程
						if (!_threadReceiveData.Join(100))
							_threadReceiveData.Abort();
						_threadReceiveData = null;
					}

					//关闭并释放TcpClient
					if (_udpClient != null)
					{
						_udpClient.Close();
						_udpClient = null;
					}

					_connected = false;
					SystemMessager.OutInfoGeneral(String.Format("Close TCPClient [{0}] Success", _config.IPPointWrite.ToString()));
					//关闭事件的通知
					ConnectChangedEvents(true, false);
				}
				catch (Exception exc)
				{
					SystemMessager.OutInfoException(String.Format("Close TCPClient [{0}]  - [ {1} ]", _config.IPPointWrite.ToString(), exc.Message));
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
					byte[] recData = _udpClient.Receive(ref _recIPEndPoint);
					if (recData != null && recData.Length > 0)
					{
						if (_recIPEndPoint != null && IPAddress.Equals(_recIPEndPoint.Address, _config.IPPointWrite.Address) && _recIPEndPoint.Port == _config.IPPointWrite.Port)
						{
							ReceivedDataEvents(recData, 0, recData.Length);
						}
					}
					else if (!_shutdownEvent.WaitOne(0))
					{
						CloseUdpClient(TcpCloseReason.RemoteDisconnection);
						SystemMessager.OutInfoError("UdpClient Received 0 Bytes, So Close It");
						return;
					}
				}
				catch (Exception exc)
				{
					CloseUdpClient(TcpCloseReason.RemoteDisconnection);
					SystemMessager.OutInfoException(exc.Message);
					return;
				}
			}
		}

		public override OutBufferType BufferTypeReceived()
		{
			return OutBufferType.UdpReceived;
		}

		public override OutBufferType BufferTypeSent()
		{
			return OutBufferType.UdpSent;
		}

		protected override int WriteDataImpl(byte[] buf, int index, int count)
		{
			//发送数据
			try
			{
				lock (_lockConnected)
				{
					if (! _connected)
					{
						return -1;
					}
				}

				byte[] buf3 = new byte[count];
				Array.Copy(buf, index, buf3, 0, count);
				_udpClient.Send(buf, buf.Length, _config.IPPointWrite);
				return buf.Length;
			}
			catch (Exception exc)
			{
				CloseUdpClient(TcpCloseReason.WriteError);
				SystemMessager.OutInfoException(exc.Message);
				return -1;
			}
		}
	
		#region 释放非托管资源
		private bool _isDisposed = false;

		~UdpClientChannel()
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
				CloseUdpClient(TcpCloseReason.Dispose);
				_isDisposed = true;
				GC.SuppressFinalize(this);
			}
		}
		#endregion

		protected override Type ChannelConfigViewType()
		{
			return typeof(UdpClientConfigWin);
		}

        protected override ChannelConfig ChannelConfig()
		{
			return _config;
		}
	}
}
