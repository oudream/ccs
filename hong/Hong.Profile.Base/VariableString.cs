using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Profile.Base
{
	public class VariableString : VariableBase
	{
		public VariableString(string variableName, String defaultValue)
			: base(variableName, defaultValue)
		{
			_defaultValue = defaultValue;
		}

		public override void SetValue(object value)
		{
			throw new NotImplementedException();
		}

		private String _defaultValue;
		public new String DefaultValue
		{
			get
			{
				return _defaultValue;
			}
		}
	}
}
