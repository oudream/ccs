using System;

namespace Hong.Profile.Base
{
	public enum VariableChangeType
	{
		ReadOnly,
		SetValue,
		RemoveVariableName,
		Other
	}

	public class VariableChangedArgs : EventArgs
	{
		// Fields
		private readonly VariableChangeType _changeType;
		private readonly VariableBase _variable;
		private readonly object _value;

		public VariableChangedArgs(VariableChangeType changeType, VariableBase variable, object value)
		{
			_changeType = changeType;
			_variable = variable;
			_value = value;
		}

		public VariableChangeType ChangeType
		{
			get
			{
				return _changeType;
			}
		}

		public VariableBase Variable
		{
			get
			{
				return _variable;
			}
		}

		public object Value
		{
			get
			{
				return _value;
			}
		}
	}

	public class VariableChangingArgs : VariableChangedArgs
	{
		private bool _cancel;

		public VariableChangingArgs(VariableChangeType changeType, VariableBase variable, object value) :
			base(changeType, variable, value)
		{
		}

		public bool Cancel
		{
			get
			{
				return _cancel;
			}
			set
			{
				_cancel = value;
			}
		}
	}

	public delegate void VariableChangingHandler(object sender, VariableChangingArgs e);

	public delegate void VariableChangedHandler(object sender, VariableChangedArgs e);
}