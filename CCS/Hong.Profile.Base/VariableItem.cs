using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Profile.Base
{
	/// <summary>
	/// Config 的变量项，目前只支持 string , Enum, 基本型
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public class VariableItem<T> : VariableBase
	{
        public VariableItem()
        {

        }

        public VariableItem(string entry, T defaultValue)
            : base(entry, defaultValue)
        {
            _defaultValue = defaultValue;
            _value = _defaultValue;
        }

        public VariableItem(string section, string entry, T defaultValue)
            : base(section, entry, defaultValue)
        {
            _defaultValue = defaultValue;
            _value = _defaultValue;
        }

        private T _defaultValue;
		public T DefaultValue
		{
			get
			{
				return _defaultValue;
			}
            set
            {
                _defaultValue = value;
            }
		}

		private T _value;
		public T Value
		{
			get
			{
				return _value;
			}
            set
            {
                SetValueSelf(value);
            }
		}

        protected override void SetValueBase(object value)
		{
			if (value == null)
			{
				//SetValueSelf(_defaultValue);
				return;
			}
			if (value is T)
			{
				SetValueSelf((T)value);
				return;
			}
			if (value is string)
			{
				object v = ConvertValue((string)value);
				if (v != null)
				{
					SetValueSelf((T)v);
				}
			}
		}

		private void SetValueSelf(T value)
		{
			if (ReadOnly)
			{
				return ;
			}
			if (! RaiseChangingEvent(VariableChangeType.SetValue, value))
			{
				return;
			}
			if (! _value.Equals(value))
			{
				valueBase_ = value;
				_value = value;

				RaiseChangedEvent(VariableChangeType.SetValue, value);
			}
		}

		private object ConvertValue(string value)
		{
			if (_value is string)
			{
				return value;
			}
			else if (_value is decimal)
			{
				return Convert.ToDecimal(value);
			}
			else if (_value is byte)
			{
				return Convert.ToByte(value);
			}
			else if (_value is char)
			{
				return Convert.ToChar(value);
			}
			else if (_value is float)
			{
				return Convert.ToSingle(value);
			}
			else if (_value is int)
			{
				return Convert.ToInt32(value);
			}
			else if (_value is bool)
			{
				return Convert.ToBoolean(value);
			}
			else if (_value is double)
			{
				return Convert.ToDouble(value);
			}
			else if (_value is DateTime)
			{
				return Convert.ToDateTime(value);
			}
			else if (_value is Enum)
			{
				return Enum.Parse(_value.GetType(), value);
			}
			return null;
		}

		public override string ToString()
		{
			return _value.ToString();
		}
	}
}
