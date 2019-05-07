using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Profile.Base
{
	public class VariableEnum : VariableBase
	{
		public VariableEnum(string variableName, object defaultValue)
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
