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
	public partial class TcpClientConfigWin : Form, IChannelConfigForm
	{
		public TcpClientConfigWin()
		{
			InitializeComponent();
		}

		#region IChannelConfigForm 成员

		public string ViewIn(Hong.Profile.Base.VariableList config)
		{
			if (!(config is TcpClientConfig))
			{
				return "";
			}
			TcpClientConfig udpClientConfig = (TcpClientConfig)config;
			udpClientConfig.IPAddressRemote.Value = this.IPAddressRemoteEd.Text;
			udpClientConfig.PortRemote.Value = Convert.ToInt32(this.PortRemoteEd.Value);
			return "";
		}

		private int _viewOutTotal = 0;
		private TcpClientConfig _ConfigFirst;
		public void ViewOut(Hong.Profile.Base.VariableList config)
		{
			if (!(config is TcpClientConfig))
			{
				return;
			}

			TcpClientConfig udpClientConfig = (TcpClientConfig)config;
			this.IPAddressRemoteEd.Text = udpClientConfig.IPAddressRemote.Value;
			this.PortRemoteEd.Value = udpClientConfig.PortRemote.Value;

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
			TcpClientConfig config = new TcpClientConfig();
			config.AssignFromDefaultValue(_ConfigFirst);
			ViewOut(config);
		}
	}
}
