using System;
using System.Collections.Generic;
using System.Text;
using Hong.Profile.Base;

namespace Hong.Channel.Base
{
	public interface IChannelConfigForm
	{
		string ViewIn(VariableList config);
		void ViewOut(VariableList config);
	}
}
