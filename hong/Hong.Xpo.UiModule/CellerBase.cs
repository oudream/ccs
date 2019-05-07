using System;
using System.Collections.Generic;
using System.Text;
using Hong.Profile.Base;

namespace Hong.Xpo.UiModule
{
    public abstract class CellerBase
    {
        public CellerBase()
        {
            _variables = new VariableList();
            _variables.Changed += new VariableListChangedHandler(VariablesChanged);

            _layout = CreateLayout();
            _layout.Changed += new VariableListChangedHandler(LayoutChanged);

            _variables.AddVariable<string>("TypeName", this.GetType().FullName);
            _parentName = Variables.AddVariable<string>("ParentName", "");
            _title = AddVariable<string>("Title", "");
        }


        #region Variables

        protected virtual void VariablesChanged(object sender, VariableListChangedArgs e)
        {
            if (e.Variable == Title)
            {
                TitleValueChanged(Title.Value);
            }
            else if (e.Variable == ParentName)
            {
                ParentNameChanged(ParentName.Value);
            }
        }

        private VariableList _variables;
        public VariableList Variables
        {
            get
            {
                return _variables;
            }
        }

        private string _sectionName;
        public string SectionName
        {
            get
            {
                return _sectionName;
            }
            set
            {
                SetSectionName(value);
            }
        }

        public string Name
        {
            get
            {
                return _sectionName;
            }
        }

        protected virtual void SetSectionName(string value)
        {
            _sectionName = value;
            foreach (VariableBase variable in _variables.Variables)
            {
                variable.Section = value;
            }
        }

        public VariableItem<T> AddVariable<T>(string entry, T defaultValue)
        {
            return Variables.AddVariable<T>(entry, defaultValue);
        }

        public virtual void VariablesIn()
        { 
        }

        public virtual void VariablesOut()
        {
        }

        #endregion

        #region Viewer
        
        private ViewerBase _viewer;
        public ViewerBase Viewer
        {
            get
            {
                return _viewer;
            }
            set
            {
                SetViewer(value);
            }
        }

        private void SetViewer(ViewerBase value)
        {
            if (_viewer != null)
            {
                _viewer.Cellers.Remove(this);
            }
            if (value != null)
            {
                value.Cellers.Add(this);
            }
            _viewer = value;
        }
        
        #endregion

        #region Parent

        private VariableItem<string> _parentName;
        public VariableItem<string> ParentName
        {
            get
            {
                return _parentName;
            }
        }

        private UiContain _parent;
        public UiContain Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                SetParent(value);
                if (_parentName.Value != value.Name)
                {
                    _parentName.Value = value.Name;
                }
            }
        }

        private void SetParent(UiContain value)
        {
            if (_parent != null)
            {
                RemoveFromContain(_parent);
                _parent.Cellers.Remove(this);
            }
            _parent = value;
            AddToContain(value);
            value.Cellers.Add(this);
        }

        protected abstract void AddToContain(UiContain contain);

        protected abstract void RemoveFromContain(UiContain contain);

        private void ParentNameChanged(string value)
        {
            if (Viewer == null)
            {
                return;
            }
            UiContain contain = Viewer.GetContain(value);
            if (contain != null)
            {
                SetParent(contain);
            }
        }

        #endregion

        #region Title

        private VariableItem<string> _title;
        public VariableItem<string> Title
        {
            get
            {
                return _title;
            }
        }

        protected abstract void TitleValueChanged(string value);

        #endregion

        private UiLayout _layout;
        public UiLayout Layout
        {
            get
            {
                return _layout;
            }
        }

        protected abstract UiLayout CreateLayout();

        protected abstract void LayoutChanged(object sender, VariableListChangedArgs e);
    }
}
