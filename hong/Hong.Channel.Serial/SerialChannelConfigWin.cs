using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using Hong.Channel.Base;

namespace Hong.Channel.Serial
{
	internal partial class SerialChannelConfigWin : Form, IChannelConfigForm
	{
		public SerialChannelConfigWin()
		{
			InitializeComponent();

			InitializePortNameEd();
		}

		private void InitializePortNameEd()
		{
			string[] portnames = SerialPort.GetPortNames();
			PortNameEd.Items.AddRange(portnames);
		}

		#region IChannelConfigForm 成员

		public string ViewIn(Hong.Profile.Base.VariableList config)
		{
			if (! (config is SerialConfig))
			{
				return "config is not SerialConfig";
			}
			SerialConfig serialConfig = (SerialConfig)config;

			RadioButton radiobutton = null;
			//通讯口
			serialConfig.PortName.Value = this.PortNameEd.Text;
			//波特率
			foreach (Control control in this.BaudRateGroupBox.Controls)
			{
				if (control is RadioButton)
				{
					radiobutton = (RadioButton)control;
					if (radiobutton.Checked)
					{
						serialConfig.BaudRate.Value = Convert.ToInt32(radiobutton.Text);
					}
				}
			}
			//数据位
			foreach (Control control in this.DataBitsGroupBox.Controls)
			{
				if (control is RadioButton)
				{
					radiobutton = (RadioButton)control;
					if (radiobutton.Checked)
					{
						serialConfig.DataBits.Value = Convert.ToInt32(radiobutton.Text);
					}
				}
			}
			//停止位
			foreach (Control control in this.StopBitsGroupBox.Controls)
			{
				if (control is RadioButton)
				{
					radiobutton = (RadioButton)control;
					if (radiobutton.Checked)
					{
						serialConfig.StopBits.Value = (StopBits)Enum.Parse(typeof(StopBits), radiobutton.Text);
					}
				}
			}
			//奇偶校验检查协议
			foreach (Control control in this.ParityGroupBox.Controls)
			{
				if (control is RadioButton)
				{
					radiobutton = (RadioButton)control;
					if (radiobutton.Checked)
					{
						serialConfig.Parity.Value = (Parity)Enum.Parse(typeof(Parity), radiobutton.Text);
					}
				}
			}
			//握手协议
			serialConfig.Handshake.Value = (Handshake)Enum.Parse(typeof(Handshake), this.HandshakeEd.Text);
			//奇偶校验错误时替换数据流中的无效字节
			serialConfig.ParityReplace.Value = (byte)this.ParityReplaceEd.Value;
			//RtsEnable
			serialConfig.RtsEnable.Value = this.RtsEnableEd.Checked;
			//DtrEnable
			serialConfig.DtrEnable.Value = this.DtrEnableEd.Checked;
			//输入缓冲区的大小
			serialConfig.ReadBufferSize.Value = Convert.ToInt32(this.ReadBufferSizeEd.Value);
			//输出缓冲区的大小
			serialConfig.WriteBufferSize.Value = Convert.ToInt32(this.WriteBufferSizeEd.Value);
			//Null 字节在端口和接收缓冲区之间传输时是否被忽略
			serialConfig.DiscardNull.Value = this.DiscardNullEd.Checked;
			//读取操作未完成时发生超时之前的毫秒数
			serialConfig.ReadTimeout.Value = Convert.ToInt32(this.ReadTimeoutEd.Value);
			//写入操作未完成时发生超时之前的毫秒数
			serialConfig.WriteTimeout.Value = Convert.ToInt32(this.WriteTimeoutEd.Value);
			return "";
		}

		private int _viewOutTotal = 0;
		private SerialConfig _ConfigFirst;
		public void ViewOut(Hong.Profile.Base.VariableList config)
		{
			if (!(config is SerialConfig))
			{
				return;
			}
			SerialConfig serialConfig = (SerialConfig)config;

			RadioButton radiobutton = null;
			//通讯口
			this.PortNameEd.SelectedIndex = this.PortNameEd.Items.IndexOf(serialConfig.PortName.Value);
			//波特率
			foreach (Control control in this.BaudRateGroupBox.Controls)
			{
				if (control is RadioButton)
				{
					radiobutton = (RadioButton)control;
					if (radiobutton.Text == serialConfig.BaudRate.ToString())
					{
						radiobutton.Checked = true;
					}
				}
			}
			//数据位
			foreach (Control control in this.DataBitsGroupBox.Controls)
			{
				if (control is RadioButton)
				{
					radiobutton = (RadioButton)control;
					if (radiobutton.Text == serialConfig.DataBits.ToString())
					{
						radiobutton.Checked = true;
					}
				}
			}
			//停止位
			foreach (Control control in this.StopBitsGroupBox.Controls)
			{
				if (control is RadioButton)
				{
					radiobutton = (RadioButton)control;
					if (radiobutton.Text == serialConfig.StopBits.ToString())
					{
						radiobutton.Checked = true;
					}
				}
			}
			//奇偶校验检查协议
			foreach (Control control in this.ParityGroupBox.Controls)
			{
				if (control is RadioButton)
				{
					radiobutton = (RadioButton)control;
					if (radiobutton.Text == serialConfig.Parity.ToString())
					{
						radiobutton.Checked = true;
					}
				}
			}
			//握手协议
			this.HandshakeEd.SelectedIndex = this.HandshakeEd.Items.IndexOf(serialConfig.Handshake.ToString());
			//奇偶校验错误时替换数据流中的无效字节
			this.ParityReplaceEd.Value = serialConfig.ParityReplace.Value;
			//RtsEnable
			this.RtsEnableEd.Checked = serialConfig.RtsEnable.Value;
			//DtrEnable
			this.DtrEnableEd.Checked = serialConfig.DtrEnable.Value;
			//输入缓冲区的大小
			this.ReadBufferSizeEd.Value = serialConfig.ReadBufferSize.Value;
			//输出缓冲区的大小
			this.WriteBufferSizeEd.Value = serialConfig.WriteBufferSize.Value;
			//Null 字节在端口和接收缓冲区之间传输时是否被忽略
			this.DiscardNullEd.Checked = serialConfig.DiscardNull.Value;
			//读取操作未完成时发生超时之前的毫秒数
			this.ReadTimeoutEd.Value = serialConfig.ReadTimeout.Value;
			//写入操作未完成时发生超时之前的毫秒数
			this.WriteTimeoutEd.Value = serialConfig.WriteTimeout.Value;

			if (_viewOutTotal == 0)
			{
				_ConfigFirst = serialConfig;
			}

			++_viewOutTotal;
		}

		#endregion

		private void button1_Click(object sender, EventArgs e)
		{
			if (this.PortNameEd.SelectedIndex < 0)
			{
				return;
			}

			this.DialogResult = DialogResult.OK;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
		}

		private void button3_Click(object sender, EventArgs e)
		{
			if (_ConfigFirst == null)
			{
				return;
			}
			ViewOut(_ConfigFirst);
		}

		private void button4_Click(object sender, EventArgs e)
		{
			if (_ConfigFirst == null)
			{
				return;
			}
			SerialConfig config = new SerialConfig();
			config.AssignFromDefaultValue(_ConfigFirst);
			ViewOut(config);
		}
	}
}
