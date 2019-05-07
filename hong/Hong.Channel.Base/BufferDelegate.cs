using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Channel.Base
{
    public delegate int ChannelReadDelegate(byte[] buf, int index, int count);

    public delegate int ChannelWriteDelegate(byte[] buf, int index, int count);

	public delegate void ConnectChangedDelegate(bool oldConnected, bool newConnected);
}
