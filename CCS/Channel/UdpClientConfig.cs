using System;
using System.Collections.Generic;
using System.Text;
using Hong.Profile.Base;
using System.Net;
using Hong.Channel.Base;

namespace Hong.Channel.NetWork
{
    public class UdpClientConfig : ChannelConfig
	{
		public UdpClientConfig() : base()
		{
			ConstructAll();
		}

		private void ConstructAll()
		{
			IPAddress address = new IPAddress(NetWorker.DefaultAddress.GetAddressBytes());
			int port = NetWorker.DefaultPort;
			IPPointWrite = new IPEndPoint(address, port);

            PortListen = AddVariable<int>("PortLocal", NetWorker.DefaultPort + 1);
            IPAddressWrite = AddVariable<string>("IPAddressWrite", NetWorker.DefaultAddress.ToString());
			PortWrite = AddVariable("PortWrite", NetWorker.DefaultPort);
		}

        protected override string SectionImpl()
		{
			return "UdpClientChannel";
		}

		public IPEndPoint IPPointWrite;

		public VariableItem<int> PortListen;

		public VariableItem<string> IPAddressWrite;

		public VariableItem<int> PortWrite;

		protected override void VariableChangingEvent(object sender, VariableChangingArgs e)
		{
			base.VariableChangingEvent(sender, e);
		}

		protected override void VariableChangedEvent(object sender, VariableChangedArgs e)
		{
			base.VariableChangedEvent(sender, e);

			if (e.Variable.Equals(IPAddressWrite))
			{
				IPAddress iPAddress;
				if (IPAddress.TryParse(IPAddressWrite.Value, out iPAddress))
				{
					IPPointWrite.Address = iPAddress;
				}
				return;
			}
			if (e.Variable.Equals(PortWrite))
			{
				IPPointWrite.Port = PortWrite.Value;
			}
		}
	}
}
