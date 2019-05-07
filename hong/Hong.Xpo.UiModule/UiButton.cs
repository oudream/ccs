using System;
using System.Collections.Generic;
using System.Text;
using Hong.Profile.Base;

namespace Hong.Xpo.UiModule
{
    public abstract class UiButton : UiControlGeneric<string>
    {
        public UiButton()
        {
            _eventName = Variables.AddVariable<string>("EventName", "");
        }

        private VariableItem<string> _eventName;
        public VariableItem<string> EventName
        {
            get
            {
                return _eventName;
            }
        }

        protected void RaiseExecute()
        {
            if (Execute != null)
            {
                Execute(this, new EventArgs());
            }
        }

        public event EventHandler Execute;
    }
}
