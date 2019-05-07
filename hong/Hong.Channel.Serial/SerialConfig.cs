using System;
using System.Collections.Generic;
using System.Text;
using Hong.Profile.Base;
using System.IO.Ports;
using Hong.Channel.Base;

namespace Hong.Channel.Serial
{
    public class SerialConfig : ChannelConfig
	{
		public SerialConfig()
			: base()
		{
			ConstructAll();
		}

		private void ConstructAll()
		{
            PortName = AddVariable<string>("PortName", "COM1");
            BaudRate = AddVariable<int>("BaudRate", 0x2580);
            DataBits = AddVariable<int>("DataBits", 8);
            StopBits = AddVariable<StopBits>("StopBits", System.IO.Ports.StopBits.One);

			Parity = AddVariable<Parity>("Parity", System.IO.Ports.Parity.None);
			Handshake = AddVariable<Handshake>("Handshake", System.IO.Ports.Handshake.None);
			ParityReplace = AddVariable<byte>("ParityReplace", 0x3F);
			RtsEnable = AddVariable<bool>("RtsEnable", false);
			DtrEnable = AddVariable<bool>("DtrEnable", false);
			ReadBufferSize = AddVariable<int>("ReadBufferSize", 0x1000);
			WriteBufferSize = AddVariable<int>("WriteBufferSize", 0x800);
			DiscardNull = AddVariable<bool>("DiscardNull", false);
			ReadTimeout = AddVariable<int>("ReadTimeout", -1);
			WriteTimeout = AddVariable<int>("WriteTimeout", -1);
		}

        protected override string SectionImpl()
		{
			return "SerialChannel";
		}

		public VariableItem<string> PortName;

		public VariableItem<int> BaudRate;

		public VariableItem<int> DataBits;

		public VariableItem<StopBits> StopBits;


		public VariableItem<Parity> Parity;

		public VariableItem<Handshake> Handshake;

		public VariableItem<byte> ParityReplace;

		public VariableItem<bool> RtsEnable;

		public VariableItem<bool> DtrEnable;

		public VariableItem<int> ReadBufferSize;

		public VariableItem<int> WriteBufferSize;

		public VariableItem<bool> DiscardNull;

		public VariableItem<int> ReadTimeout;

		public VariableItem<int> WriteTimeout;

		//public bool BreakState { get; set; }

		//public int BytesToRead { get; }

		//public int BytesToWrite { get; }
	
		//private int CachedBytesToRead { get; }

		//public bool CDHolding { get; }

		//public bool CtsHolding { get; }

		//public bool DsrHolding { get; }

		//public Encoding Encoding { get; set; }

		//public bool IsOpen { get; }

		//public string NewLine { get; set; }

		//public int ReceivedBytesThreshold { get; set; }
	}
}
