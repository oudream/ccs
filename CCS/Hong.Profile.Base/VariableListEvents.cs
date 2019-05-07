using System;

namespace Hong.Profile.Base
{
	public enum VariableListChangeType
	{
		ReadOnly,
		SetValue,
		RemoveVariable,
		Other
	}

	public class VariableListChangedArgs : EventArgs
	{
		// Fields
		private readonly VariableListChangeType _changeType;
        private readonly VariableList _variableList;
		private readonly VariableBase _variable;
		private readonly object _value;

        public VariableListChangedArgs(VariableListChangeType changeType, VariableList variableList, VariableBase variable, object value)
		{
			_changeType = changeType;
            _variableList = variableList;
			_variable = variable;
			_value = value;
		}

		public VariableListChangeType ChangeType
		{
			get
			{
				return _changeType;
			}
		}

        public VariableList VariableList
		{
			get
			{
                return _variableList;
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

	public class VariableListChangingArgs : VariableListChangedArgs
	{
		private bool _cancel;

        public VariableListChangingArgs(VariableListChangeType changeType, VariableList variableList, VariableBase variable, object value) :
            base(changeType, variableList, variable, value)
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

	public delegate void VariableListChangingHandler(object sender, VariableListChangingArgs e);

    public delegate void VariableListChangedHandler(object sender, VariableListChangedArgs e);
}