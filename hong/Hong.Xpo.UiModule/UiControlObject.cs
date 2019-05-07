using System;
using System.Collections.Generic;
using System.Text;
using Hong.Profile.Base;

namespace Hong.Xpo.UiModule
{
    public abstract class UiControlObject : CellerBase
    {
        public UiControlObject()
        {
            _propertyName = AddVariable<string>("PropertyName", "");
        }

        private VariableItem<string> _propertyName;
        public VariableItem<string> PropertyName
        {
            get
            {
                return _propertyName;
            }
        }

        public object ValueBase
        {
            get
            {
                return GetValueBase();
            }
            set
            {
                SetValueBase(value);
            }
        }

        protected abstract object GetValueBase();

        protected abstract void SetValueBase(object value);

        public virtual void Initialization(Type valueType)
        { 
        }
    }
}
