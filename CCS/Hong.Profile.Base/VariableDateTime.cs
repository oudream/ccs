using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Profile.Base
{
	public class VariableDateTime : VariableBase
	{
		public VariableDateTime(string variableName, DateTime defaultValue)
			: base(variableName, defaultValue)
		{
			_defaultValue = defaultValue;
		}

		public override void SetValue(object value)
		{
			throw new NotImplementedException();
		}

		private DateTime _defaultValue;
		public new DateTime DefaultValue
		{
			get
			{
				return _defaultValue;
			}
		}
	}
}
