using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Hong.Common.Systemer;

namespace Hong.Channel.NetWork
{

	public class TcpListenerClient : IDisposable
	{
		private object _lockConnected;
		private bool _connected;
		private TcpClient _tcpClient;
		private Thread _threadReceiveData;
		private ManualResetEvent _shutdownEvent;
		private byte[] _recData;
		private int _recLen;

		public TcpListenerClient(TcpListenerChannel parent, TcpClient tcpClient)
		{
			_tcpParent = parent;
			_tcpClient = tcpClient;
			_iPPointRemote = (IPEndPoint)tcpClient.Client.RemoteEndPoint;
			_addressPortRemote = tcpClient.Client.RemoteEndPoint.ToString();

			_lockConnected = new object();
			_shutdownEvent = new ManualResetEvent(false);
		}

		private TcpListenerChannel _tcpParent;
		public TcpListenerChannel TcpParent
		{
			get
			{
				return _tcpParent;
			}
		}

		private IPEndPoint _iPPointRemote;
		public IPEndPoint IPPointRemote
		{
			get
			{
				return _iPPointRemote;
			}
		}

		private string _addressPortRemote;
		public string AddressPortRemote
		{
			get
			{
				return _addressPortRemote;
			}
		}

		internal bool StartTcpClient()
		{
			lock (_lockConnected)
			{
				try
				{
					_recData = new byte[_tcpClient.ReceiveBufferSize];
					_recLen = 0;

					//_tcpClient.SendBufferSize = 1024000;

					_shutdownEvent.Reset();
					_threadReceiveData = new Thread(new ThreadStart(ThreadingReceiveData));
					_threadReceiveData.IsBackground = true;
					_threadReceiveData.Start();
					_connected = true;
					SystemMessager.OutInfoGeneral(String.Format("Start TCPListenerClient [{0}] Success", _addressPortRemote));
				}
				catch (Exception exc)
				{
					SystemMessager.OutInfoException(String.Format("Start TCPListenerClient [{0} - [ {1} ] ", _addressPortRemote, exc.Message));
				}
			}
			return _connected;
		}

		internal bool StopTcpClient(bool isRemoteDisconnection)
		{
			lock (_lockConnected)
			{
				try
				{
					//发送停止信号
					_shutdownEvent.Set();

					//停止接收线程
					if (! isRemoteDisconnection && _threadReceiveData != null)
					{
						if (_tcpClient != null)
						{
							_tcpClient.Client.Shutdown(SocketShutdown.Both);
						}
						if (!_threadReceiveData.Join(3000))
							_threadReceiveData.Abort();
						_threadReceiveData = null;
					}
					if (_tcpClient != null)
					{
						_tcpClient.Close();
						_tcpClient = null;
					}
					_connected = false;
					SystemMessager.OutInfoGeneral(String.Format("Stop TCPListenerClient [{0}] Success", _addressPortRemote));
				}
				catch (Exception exc)
				{
					SystemMessager.OutInfoException(String.Format("Stop TCPListenerClient [{0} - [ {1} ]", _addressPortRemote, exc.Message));
				}
			}
			return !_connected;
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
								ReceivedDataImpl(_recData, 0, _recLen);
							}
							else if (! _shutdownEvent.WaitOne(0))
							{
								TcpListenerClientErrorEvents(TcpListenerClientErrorType.ReceiveDataEmpty);
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
					TcpListenerClientErrorEvents(TcpListenerClientErrorType.ReceiveDataEmpty);
					SystemMessager.OutInfoException(exc.Message);
				}
			}
		}

		#region 读取数据
		internal delegate int TcpListenerClientReadDelegate(TcpListenerClient tcpListenerClient, byte[] buf, int index, int count);
		internal TcpListenerClientReadDelegate ReceivedDataing;
		private void ReceivedDataImpl(byte[] buf, int index, int count)
		{
			if (ReceivedDataing != null)
			{
				ReceivedDataing(this, buf, index, count);
			}
		}
		#endregion

		#region 数据错误处理
		internal enum TcpListenerClientErrorType
		{
			ReceiveDataEmpty,
			WriteError
		}
		internal delegate void TcpListenerClientErrorDelegate(TcpListenerClient tcpListenerClient, TcpListenerClientErrorType errorType);
		internal TcpListenerClientErrorDelegate TCPListenerClientErroring;
		private void TcpListenerClientErrorEvents(TcpListenerClientErrorType errorType)
		{
			if (TCPListenerClientErroring != null)
			{
				TCPListenerClientErroring(this, errorType);
			}
		}
		#endregion

		internal int WriteDataImpl(byte[] buf, int index, int count)
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
				TcpListenerClientErrorEvents(TcpListenerClientErrorType.WriteError);
				SystemMessager.OutInfoException(exc.Message);
				return -1;
			}
		}

		#region 释放非托管资源
		private bool _isDisposed = false;

		~TcpListenerClient()
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
				StopTcpClient(false);
				_isDisposed = true;
				GC.SuppressFinalize(this);
			}
		}
		#endregion
	}
}
