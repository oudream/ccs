using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Hong.Channel.NetWork
{
	public class NetWorker
	{
		private static IPAddress _defaultAddress;
		public static IPAddress DefaultAddress
		{
			get
			{
				if (_defaultAddress == null)
				{
					_defaultAddress = IPAddress.Parse("127.0.0.1");
				}
				return _defaultAddress;
			}
		}

		public const int DefaultPort = 5566;
	}
}
