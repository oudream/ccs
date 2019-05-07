using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Profile.Base
{
	public class VariableInt : VariableBase
	{
		public VariableInt(string variableName, int defaultValue)
			: base(variableName, defaultValue)
		{
			_defaultValue = defaultValue;
		}

		public override void SetValue(object value)
		{
			throw new NotImplementedException();
		}

		private int _defaultValue;
		public new int DefaultValue
		{
			get
			{
				return _defaultValue;
			}
		}
	}
}
