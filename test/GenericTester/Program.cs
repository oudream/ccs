using System;
using System.Collections.Generic;
using System.Text;

namespace GenericTester
{
	public class VariableItem<T>
	{
		public VariableItem(string variableName, T defaultValue)
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
		}

		private T _value;
		public T Value
		{
			get
			{
				return _value;
			}
		}

		public void SetValue(object value)
		{
			if (value == null)
			{
				_value = _defaultValue;
				return;
			}
			if (value is T)
			{
				_value = (T)value;
				return;
			}
			if (value is string)
			{
				object v = GetValue((string)value);
				if (v != null)
				{
					_value = (T)v;
				}
			}
		}

		private object GetValue(string value)
		{
			try
			{
				if (_value is int)
				{
					return Convert.ToInt32(value);
				}
			}
			catch
			{
			}
			return null;
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			VariableItem<int> v = new VariableItem<int>("name", 3);

			v.SetValue("6C");

			Console.WriteLine(v.Value.ToString());

			Console.Read();
		}
	}
}
