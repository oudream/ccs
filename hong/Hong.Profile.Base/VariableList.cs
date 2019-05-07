using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Profile.Base
{
	public class VariableList
	{
        public VariableList()
        {
            _variables = new List<VariableBase>();
        }

        private List<VariableBase> _variables;
        public List<VariableBase> Variables
        {
            get
            {
                return _variables;
            }
        }

        private string _defaultSection = "";
        public string DefaultSection
        {
            get
            {
                return _defaultSection;
            }
            set
            {
                _defaultSection = value;
            }
        }

        public void AddVariable(VariableBase variable)
        {
            _variables.Add(variable);
            variable.Changing += new VariableChangingHandler(VariableChangingEvent);
            variable.Changed += new VariableChangedHandler(VariableChangedEvent);
        }

        public VariableItem<T> AddVariable<T>(string section, string entry, T defaultValue)
        {
            VariableItem<T> variable = new VariableItem<T>(section, entry, defaultValue);
            _variables.Add(variable);
            variable.Changing += new VariableChangingHandler(VariableChangingEvent);
            variable.Changed += new VariableChangedHandler(VariableChangedEvent);
            return variable;
        }

        public VariableItem<T> AddVariable<T>(string entry, T defaultValue)
        {
            return AddVariable<T>(_defaultSection, entry, defaultValue);
        }

        public void AddRange(VariableList variableList)
        {
            _variables.AddRange(variableList.Variables);
        }

        public void RemoveVariable(VariableBase variable)
		{
			foreach (VariableBase vr in _variables)
			{
				if (vr.Equals(variable))
				{
					vr.Changing -= VariableChangingEvent;
					vr.Changed -= VariableChangedEvent;
					_variables.Remove(vr);
				}
			}
		}

		public void ClearVariable()
		{
			foreach (VariableBase vr in _variables)
			{
				vr.Changing -= VariableChangingEvent;
				vr.Changed -= VariableChangedEvent;
				_variables.Remove(vr);
			}
		}

		public void LoadVariablesValue(ProfileBase profile)
		{
            if (profile == null)
            {
                return;
            }
			object value;
			foreach (VariableBase vr in _variables)
			{
                if (vr.Section != "" && vr.Entry != "")
                {
                    value = profile.GetValue(vr.Section, vr.Entry);
                    if (value != null)
                    {
                        vr.ValueBase = value;
                    }
                    else
                    {
                        vr.ValueBase = vr.DefaultValueBase;
                    }
                }
                
			}
		}

        public void SaveVariablesValue(ProfileBase profile)
		{
            if (profile == null)
            {
                return;
            }
			foreach (VariableBase vr in _variables)
			{
                profile.SetValue(vr.Section, vr.Entry, vr.ValueBase);
			}
		}

		protected VariableListChangeType GetVariableListChangeType(VariableChangeType changeType)
		{
			switch (changeType)
			{
				case VariableChangeType.ReadOnly:
					return VariableListChangeType.ReadOnly;
				case VariableChangeType.SetValue:
					return VariableListChangeType.SetValue;
				case VariableChangeType.RemoveVariableName:
					return VariableListChangeType.RemoveVariable;
				default:
					return VariableListChangeType.Other;
			}
		}

		protected virtual void VariableChangingEvent(object sender, VariableChangingArgs e)
		{
			e.Cancel = RaiseChangingEvent(GetVariableListChangeType(e.ChangeType), e.Variable, e.Value);
		}

		protected virtual void VariableChangedEvent(object sender, VariableChangedArgs e)
		{
			RaiseChangedEvent(GetVariableListChangeType(e.ChangeType), e.Variable, e.Value);
		}

        public event VariableListChangingHandler Changing;

        public event VariableListChangedHandler Changed;

		protected bool RaiseChangingEvent(VariableListChangeType changeType, VariableBase variable, object value)
		{
			VariableListChangingArgs e = new VariableListChangingArgs(changeType, this, variable, value);
			OnChanging(e);
			return !e.Cancel;
		}

		protected void RaiseChangedEvent(VariableListChangeType changeType, VariableBase variable, object value)
		{
			OnChanged(new VariableListChangedArgs(changeType, this, variable, value));
		}

		private void OnChanging(VariableListChangingArgs e)
		{
			if (Changing == null)
			{
				e.Cancel = true;
				return;
			}

            foreach (VariableListChangingHandler handler in Changing.GetInvocationList())
			{
				handler(this, e);

				// If a particular handler cancels the event, stop
				if (e.Cancel)
					break;
			}
		}

		private void OnChanged(VariableListChangedArgs e)
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

				if (!RaiseChangingEvent(VariableListChangeType.ReadOnly, null, value))
					return;

				_readOnly = value;
				RaiseChangedEvent(VariableListChangeType.ReadOnly, null, value);
			}
		}

        public VariableBase GetVariable(string section, string entry)
		{
			foreach (VariableBase item in _variables)
			{
				if (item.Section.Equals(section) && item.Entry.Equals(entry))
				{
					return item;
				}
			}
			return null;
		}

        public VariableBase GetVariableFirst(string entry)
        {
            foreach (VariableBase variable in _variables)
            {
                if (variable.Entry.Equals(entry))
                {
                    return variable;
                }
            }
            return null;
        }

		public void AssignFromDefaultValue(VariableList source)
		{
			if (this.GetType() != source.GetType())
			{
				return;
			}
			VariableBase vb;
			foreach (VariableBase item in _variables)
			{
				vb = source.GetVariable(item.Section, item.Entry);
				if (vb.GetType() != item.GetType())
				{
					item.ValueBase = vb.DefaultValueBase;
				}
			}
		}
	}
}
