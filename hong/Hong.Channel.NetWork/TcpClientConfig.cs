using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Hong.Profile.Base;
using Hong.Channel.Base;

namespace Hong.Channel.NetWork
{
    public class TcpClientConfig : ChannelConfig
	{
		public TcpClientConfig() : base()
		{
			ConstructAll();
		}

		private void ConstructAll()
		{
			IPAddress address = new IPAddress(NetWorker.DefaultAddress.GetAddressBytes());
			int port = NetWorker.DefaultPort;
			IPPointRemote = new IPEndPoint(address, port);

			IPAddressRemote = AddVariable<string>("IPAddressRemote", NetWorker.DefaultAddress.ToString());
            PortRemote = AddVariable<int>("PortRemote", NetWorker.DefaultPort);
		}

        protected override string SectionImpl()
		{
			return "TcpClientChannel";
		}

		public IPEndPoint IPPointRemote;

		public VariableItem<string> IPAddressRemote;

		public VariableItem<int> PortRemote;

		protected override void VariableChangingEvent(object sender, VariableChangingArgs e)
		{
			base.VariableChangingEvent(sender, e);
		}

		protected override void VariableChangedEvent(object sender, VariableChangedArgs e)
		{
			base.VariableChangedEvent(sender, e);

			if (e.Variable.Equals(IPAddressRemote))
			{
				IPAddress iPAddress;
				if (IPAddress.TryParse(IPAddressRemote.Value, out iPAddress))
				{
					IPPointRemote.Address = iPAddress;
				}
				return;
			}
			if (e.Variable.Equals(PortRemote))
			{
				IPPointRemote.Port = PortRemote.Value;
			}
		}
	}
}
