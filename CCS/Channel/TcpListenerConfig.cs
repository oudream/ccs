using System;
using System.Collections.Generic;
using System.Text;
using Hong.Channel.Base;
using Hong.Profile.Base;

namespace Hong.Channel.NetWork
{
    public class TcpListenerConfig : ChannelConfig
	{
		public TcpListenerConfig()
			: base()
		{
			ConstructAll();
		}

		private void ConstructAll()
		{
            PortListen = AddVariable<int>("PortLocal", NetWorker.DefaultPort + 1);
		}

        protected override string SectionImpl()
		{
			return "TcpListenerChannel";
		}

		public VariableItem<int> PortListen;
	}
}
