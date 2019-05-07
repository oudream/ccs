using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using Hong.Common.Stringer;
using Hong.Common.Systemer;
using Hong.Channel.Serial;
using Hong.Channel.Base;
using Hong.Channel.NetWork;
using System.Reflection;

namespace Hong.Channel.SerialTester
{
    public partial class ChannelTesterWin : Form
    {
		private OutInfoDelegate OutInfoing;
		private OutBufferDelegate OutBuffering;
		private ChannelBase _channel;
		private ConnectChangedDelegate ConnectChanging;
		private ChannelReadDelegate ReceivingData;

        public ChannelTesterWin()
        {
            InitializeComponent();

			OutInfoing = new OutInfoDelegate(OutInfoWin);
			SystemMessager.OutInfoed += new OutInfoDelegate(OutInfoEvent);
			OutBuffering = new OutBufferDelegate(OutBufferWin);
			SystemMessager.OutBuffered += new OutBufferDelegate(OutBufferedEvent);
        }

		private void ConnectChangeEvent(bool oldConnected, bool newConnected)
		{
			this.Invoke(ConnectChanging, oldConnected, newConnected);
		}

		private void ConnectChangeWin(bool oldConnected, bool newConnected)
		{
			this.ChannelTypeEd.Enabled = !newConnected;
			this.button1.Enabled = !newConnected;
			this.button2.Enabled = newConnected;
		}

		private int ReceivedDataEvent(byte[] buf, int index, int count)
		{
			this.Invoke(ReceivingData, buf, index, count);
			return count;
		}

		private int ReceiveDataWin(byte[] buf, int index, int count)
		{
			return count;
		}

		private void OutInfoEvent(OutInfoType infoType, string info)
		{
			this.Invoke(OutInfoing, infoType, info);
		}

		private void OutInfoWin(OutInfoType infoType, string info)
		{
			this.InfoTextBox.AppendText(info + "\r\n\r\n");
		}

		private void OutBufferedEvent(OutBufferType bufferType, string bufferStr)
		{
			OutBufferType[] bufferTypes = new OutBufferType[4] 
				{	OutBufferType.SerialReceived, 
					OutBufferType.TcpClientReceived , 
					OutBufferType.TcpServerReceived ,
					OutBufferType.UdpReceived
				};
			if (Array.IndexOf(bufferTypes, bufferType) >= 0)
			{
				this.Invoke(OutBuffering, bufferType, bufferStr);
			}
		}

		private void OutBufferWin(OutBufferType bufferType, string bufferStr)
		{
			this.BufferTextBox.AppendText(bufferStr + "\r\n\r\n");
			this.toolStripStatusLabel2.Text = _channel.ReceivedByteCount.ToString();
		}

		private void numericUpDown1_ValueChanged(object sender, EventArgs e)
		{
			this.timer1.Interval = (int)this.numericUpDown1.Value;
		}

		private void checkBox1_CheckedChanged(object sender, EventArgs e)
		{
			this.timer1.Enabled = checkBox1.Checked;
			SendDataAsTextEd.Enabled = !this.timer1.Enabled;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (_channel != null)
			{
				_channel.ShowConfig();
			}
		}

		private void button4_Click(object sender, EventArgs e)
		{
			SendData();
		}

		private byte[] MyBuffer = new byte[51200];
		private void SendData()
		{
			if (_channel == null)
			{
				return;
			}
			//_channel.WriteData(MyBuffer, 0, 51200);
			//this.toolStripStatusLabel4.Text = _channel.SentByteCount.ToString();
			//return;
			string sendText = this.SendTextBox.Text;
			if (this.SendDataAsTextEd.Checked)
			{
				_channel.WriteText(sendText);
			}
			else
			{
				if (StringHexer.IsValidHexString(sendText))
				{
					_channel.WriteHexString(sendText);
				}
				else
				{
					SystemMessager.OutInfoError("发送区的文本不是十六进制字符串");
				}
			}
			this.toolStripStatusLabel4.Text = _channel.SentByteCount.ToString();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (_channel != null)
			{
				_channel.Connected = true;
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.checkBox1.Checked = false;
			timer1.Enabled = false;
			if (_channel != null)
			{
				_channel.Connected = false;
			}
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			SendData();
		}

		private void RecieveTextBox_TextChanged(object sender, EventArgs e)
		{
			if (this.BufferTextBox.Text.Length > this.numericUpDown2.Value)
			{
				this.BufferTextBox.Clear();
			}
		}

		private void SerialTesterWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (_channel != null)
			{
				_channel.ConnectChanged -= ConnectChangeEvent;
				_channel.ReceivedData -= ReceivedDataEvent;
			}
			SystemMessager.OutInfoed -= OutInfoEvent;
			SystemMessager.OutBuffered -= OutBufferedEvent;

			this.checkBox1.Checked = false;
			timer1.Enabled = false;
			if (_channel != null)
			{
				_channel.Connected = false;
			}
		}

		private void ChannelTypeEd_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (_channel != null)
			{
				_channel.Connected = false;
				_channel.ConnectChanged -= ConnectChangeEvent;
				_channel.ReceivedData -= ReceivedDataEvent;
			}

			_channel = null;

			switch (this.ChannelTypeEd.SelectedIndex)
			{
				case 0:
					_channel = new SerialChannel();
					break;
				case 1:
					_channel = new TcpClientChannel();
					break;
				case 2:
					_channel = new TcpListenerChannel();
					break;
				case 3:
					_channel = new UdpClientChannel();
					break;
				default:
					break;
			}

			if (_channel != null)
			{
				ConnectChanging = new ConnectChangedDelegate(ConnectChangeWin);
				_channel.ConnectChanged += new ConnectChangedDelegate(ConnectChangeEvent);
				ReceivingData = new ChannelReadDelegate(ReceiveDataWin);
				_channel.ReceivedData += new ChannelReadDelegate(ReceivedDataEvent);
			}
		}
    }
}
