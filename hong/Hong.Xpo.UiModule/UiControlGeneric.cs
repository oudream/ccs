using System;
using System.Collections.Generic;
using System.Text;
using Hong.Profile.Base;

namespace Hong.Xpo.UiModule
{
    public abstract class UiControlGeneric<T> : UiControlObject
    {
        public UiControlGeneric()
        {
            
            _value = DefaultValue;
        }

        private T _value;
        public T Value
        {
            get
            {
                return GetValue();
            }
            set
            {
                SetValue(value);
            }
        }

        private T GetValue()
        {
            T value;
            if (ComponentToValueImpl(out value))
            {
                _value = value;
            }
            return _value;
        }

        private void SetValue(T value)
        {
            if (ValueToComponentImpl(value))
            {
                _value = value;
            }
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

        protected override object GetValueBase()
        {
            return Value;
        }

        protected override void SetValueBase(object value)
        {
            if (value is T)
            {
                Value = (T)value;
            }
        }

        protected abstract bool ComponentToValueImpl(out T value);

        protected abstract bool ValueToComponentImpl(T value);

    }
}
