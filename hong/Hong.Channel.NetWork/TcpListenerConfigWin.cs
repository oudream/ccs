using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Hong.Channel.Base;

namespace Hong.Channel.NetWork
{
	public partial class TcpListenerConfigWin : Form, IChannelConfigForm
	{
		public TcpListenerConfigWin()
		{
			InitializeComponent();
		}

		#region IChannelConfigForm 成员

		public string ViewIn(Hong.Profile.Base.VariableList config)
		{
			if (!(config is TcpListenerConfig))
			{
				return "";
			}
			TcpListenerConfig udpClientConfig = (TcpListenerConfig)config;
			udpClientConfig.PortListen.Value = Convert.ToInt32(this.PortListenEd.Value);
			return "";
		}

		private int _viewOutTotal = 0;
		private TcpListenerConfig _ConfigFirst;
		public void ViewOut(Hong.Profile.Base.VariableList config)
		{
			if (!(config is TcpListenerConfig))
			{
				return;
			}

			TcpListenerConfig udpClientConfig = (TcpListenerConfig)config;
			this.PortListenEd.Value = udpClientConfig.PortListen.Value;

			if (_viewOutTotal == 0)
			{
				_ConfigFirst = udpClientConfig;
			}

			++_viewOutTotal;
		}

		#endregion

		private void button1_Click(object sender, EventArgs e)
		{
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
			TcpListenerConfig config = new TcpListenerConfig();
			config.AssignFromDefaultValue(_ConfigFirst);
			ViewOut(config);
		}
	}
}
