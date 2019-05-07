using System;
using System.Collections.Generic;
using System.Text;
using Hong.Profile.Base;

namespace Hong.Xpo.UiModule
{
    public abstract class UiLayout
    {
        public UiLayout(CellerBase celler)
        {
            _celler = celler;
            _celler.Variables.Changed += new VariableListChangedHandler(VariablesChanged);
            _variables = new List<VariableBase>();
        }

        protected abstract void VariablesChanged(object sender, VariableListChangedArgs e);

        protected void RaiseChanged(VariableListChangedArgs e)
        {
            OnChanged(e);
        }

        private void OnChanged(VariableListChangedArgs e)
        {
            if (Changed != null)
            {
                Changed(this, e);
            }
        }

        public event VariableListChangedHandler Changed;

        private CellerBase _celler;
        public CellerBase Celler
        {
            get
            {
                return _celler;
            }
        }

        private List<VariableBase> _variables;
        public List<VariableBase> Variables
        {
            get
            {
                return _variables;
            }
        }

        public VariableItem<T> AddVariable<T>(string entry, T defaultValue)
        {
            VariableItem<T> variable = _celler.AddVariable<T>(entry, defaultValue);
            _variables.Add(variable);
            return variable;
        }
    }
}
