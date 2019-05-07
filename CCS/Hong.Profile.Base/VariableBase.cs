using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Profile.Base
{
	public abstract class VariableBase
	{
        public VariableBase()
        {

        }

        public VariableBase(string entry, object defaultValue)
        {
            _entry = entry;
            _defaultValueBase = defaultValue;
            valueBase_ = _defaultValueBase;
        }

        public VariableBase(string section, string entry, object defaultValue)
        {
            _section = section;
            _entry = entry;
            _defaultValueBase = defaultValue;
            valueBase_ = _defaultValueBase;
        }

        private string _section;
        public string Section
        {
            get
            {
                return _section;
            }
            set
            {
                _section = value;
            }
        }

        private string _entry;
        public string Entry
        {
            get
            {
                return _entry;
            }
            set
            {
                _entry = value;
            }
        }

        private object _defaultValueBase;
		public object DefaultValueBase
		{
			get
			{
				return _defaultValueBase;
			}
            set
            {
                _defaultValueBase = value;
            }
		}

        protected object valueBase_;
		public object ValueBase
		{
			get
			{
				return valueBase_;
			}
            set
            {
                SetValueBase(value);
            }
		}

		protected abstract void SetValueBase(object value);

		public event VariableChangingHandler Changing;

		public event VariableChangedHandler Changed;

		protected bool RaiseChangingEvent(VariableChangeType changeType, object value)
		{
			VariableChangingArgs e = new VariableChangingArgs(changeType, this, value);
			OnChanging(e);
			return !e.Cancel;
		}

		protected void RaiseChangedEvent(VariableChangeType changeType, object value)
		{
			OnChanged(new VariableChangedArgs(changeType, this, value));
		}

		protected virtual void OnChanging(VariableChangingArgs e)
		{
			if (Changing == null)
			{
				e.Cancel = true;
				return;
			}

			foreach (VariableChangingHandler handler in Changing.GetInvocationList())
			{
				handler(this, e);

				// If a particular handler cancels the event, stop
				if (e.Cancel)
					break;
			}
		}

		protected virtual void OnChanged(VariableChangedArgs e)
		{
			if (Changed != null)
				Changed(this, e);
		}

		private bool _readOnly;
		public bool ReadOnly
		{
			get
			{
				return _readOnly;
			}

			set
			{
				if (_readOnly == value)
					return;

				if (!RaiseChangingEvent(VariableChangeType.ReadOnly, value))
					return;

				_readOnly = value;
				RaiseChangedEvent(VariableChangeType.ReadOnly, value);
			}
		}
	}
}
