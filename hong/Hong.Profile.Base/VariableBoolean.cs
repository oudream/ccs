using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Profile.Base
{
	public class VariableBoolean : VariableBase
	{
		public VariableBoolean(string variableName, Boolean defaultValue)
			: base(variableName, defaultValue)
		{
			_defaultValue = defaultValue;
		}

		public override void SetValue(object value)
		{
			throw new NotImplementedException();
		}

		private Boolean _defaultValue;
		public new Boolean DefaultValue
		{
			get
			{
				return _defaultValue;
			}
		}
	}
}
