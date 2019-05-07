using System;
using System.Collections.Generic;
using System.Text;
using Hong.Channel.Base;
using System.IO.Ports;
using Hong.Common.Systemer;
using System.Windows.Forms;
using Hong.Common.Stringer;
using System.Threading;
using Hong.Profile.Base;

namespace Hong.Channel.Serial
{
    public class SerialChannel : ChannelBase
    {
        private SerialPort _serialPort;
		private byte[] _recData;
		private int _recLen;
		private SerialConfig _config;

        public SerialChannel()
        {
            _serialPort = new SerialPort();
			_serialPort.ErrorReceived += new SerialErrorReceivedEventHandler(_serialPort_ErrorReceived);
			_serialPort.DataReceived += new SerialDataReceivedEventHandler(_serialPort_DataReceived);

			_config = new SerialConfig();
        }

		protected override Type ChannelConfigViewType()
		{
			return typeof(SerialChannelConfigWin);
		}

		protected override ChannelConfig ChannelConfig()
		{
			return _config;
		}

		public override OutBufferType BufferTypeReceived()
		{
			return OutBufferType.SerialReceived;
		}

		public override OutBufferType BufferTypeSent()
		{
			return OutBufferType.SerialSent;
		}

		public override bool Connected
		{
			get
			{
				return _serialPort.IsOpen;
			}
			set
			{
				if (value != _serialPort.IsOpen)
				{
					if (value)
					{
						OpenSerial();
					}
					else
					{
						CloseSerial();
					}
				}
			}
		}

		private void OpenSerial()
		{
			if (!_serialPort.IsOpen)
			{
				try
				{
					_recData = new byte[_serialPort.ReadBufferSize];
					_recLen = 0;
					_serialPort.Open();
					SystemMessager.OutInfoGeneral("Open Serial " + _serialPort.PortName + " Sucess");
					ConnectChangedSerial(false, _serialPort.IsOpen);
				}
				catch (Exception exc)
				{
					SystemMessager.OutInfoException(String.Format("Open Serial {0} : {1}", _serialPort.PortName, exc.Message));
				}
			}
		}

		private void CloseSerial()
		{
			if (_serialPort.IsOpen)
			{
				try
				{
					_serialPort.Close();
					SystemMessager.OutInfoGeneral("Close Serial " + _serialPort.PortName + " Sucess");
					ConnectChangedSerial(true, _serialPort.IsOpen);
				}
				catch (Exception exc)
				{
					SystemMessager.OutInfoException(String.Format("Close Serial {0} : {1}", _serialPort.PortName, exc.Message));
				}
			}
		}

		private void ConnectChangedSerial(bool oldConnected, bool newConnected)
		{
			ConnectChangedEvents(oldConnected, newConnected);
		}

		private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
		{
			try
			{
				//接收数据
				_recLen = _serialPort.Read(_recData, 0, _recData.Length);
				//byte[] buf = new byte[len];
				//Array.Copy(_recData, 0, buf, 0, len);
				ReceivedDataEvents(_recData, 0, _recLen);
			}
			catch (Exception exc)
			{
				SystemMessager.OutInfoException(exc.Message);
			}
		}

		protected override int WriteDataImpl(byte[] buf, int index, int count)
		{
			try
			{
				_serialPort.Write(buf, index, count);
				return count;
			}
			catch (Exception e)
			{
				SystemMessager.OutInfoException(e.Message);
				Connected = false;
				return 0;
			}
		}

		private void _serialPort_ErrorReceived(object sender, SerialErrorReceivedEventArgs e)
		{
			CloseSerial();
		}
	}
}
