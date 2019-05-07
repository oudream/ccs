using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Xpo.Module
{
    /*
    public abstract class XpobjectProfile
    {
        public XpobjectProfile()
        {
            _fieldDescribes = new List<FieldDescribe>();

            _fieldDescribesIndex = AddVariable<string>("FieldDescribesIndex", "");
            _fieldDescribesName = AddVariable<string>("FieldDescribesName", "");
            _fieldDescribesTitle = AddVariable<string>("FieldDescribesTitle", "");
            _fieldDescribesLength = AddVariable<string>("FieldDescribesLength", "");
            _fieldDescribesVisible = AddVariable<string>("FieldDescribesVisible", "");
        }

        #region Field Describe
        private List<FieldDescribe> _fieldDescribes;
        public List<FieldDescribe> FieldDescribes
        {
            get
            {
                return _fieldDescribes;
            }
        }

        private VariableItem<string> _fieldDescribesIndex;
        public VariableItem<string> FieldDescribesIndex
        {
            get
            {
                return _fieldDescribesIndex;
            }
        }

        private VariableItem<string> _fieldDescribesName;
        public VariableItem<string> FieldDescribesName
        {
            get
            {
                return _fieldDescribesName;
            }
        }

        private VariableItem<string> _fieldDescribesTitle;
        public VariableItem<string> FieldDescribesTitle
        {
            get
            {
                return _fieldDescribesTitle;
            }
        }

        private VariableItem<string> _fieldDescribesLength;
        public VariableItem<string> FieldDescribesLength
        {
            get
            {
                return _fieldDescribesLength;
            }
        }

        private VariableItem<string> _fieldDescribesVisible;
        public VariableItem<string> FieldDescribesVisible
        {
            get
            {
                return _fieldDescribesVisible;
            }
        }

        protected override void VariablesChanged(object sender, VariableListChangedArgs e)
        {
            base.VariablesChanged(sender, e);

            if (e.Variable == _fieldDescribesIndex || e.Variable == _fieldDescribesName || e.Variable == _fieldDescribesTitle || e.Variable == _fieldDescribesLength || e.Variable == _fieldDescribesVisible)
            {
                if (_fieldDescribesIndex.Value == "" || _fieldDescribesName.Value == "" || _fieldDescribesTitle.Value == "" || _fieldDescribesLength.Value == "" || _fieldDescribesVisible.Value == "")
                {
                }
                else
                {
                    if (_autoCreatingFieldDescribes)
                    {
                        return;
                    }
                    char[] sparator = { ' ', ',', ';' };
                    string[] fieldindexes = _fieldDescribesIndex.Value.Split(sparator);
                    string[] fieldNames = _fieldDescribesName.Value.Split(sparator);
                    string[] fieldTitles = _fieldDescribesTitle.Value.Split(sparator);
                    string[] fieldLengths = _fieldDescribesLength.Value.Split(sparator);
                    string[] fieldVisibles = _fieldDescribesVisible.Value.Split(sparator);
                    if (fieldindexes.Length == fieldNames.Length && fieldindexes.Length == fieldNames.Length && fieldindexes.Length == fieldTitles.Length && fieldindexes.Length == fieldLengths.Length && fieldindexes.Length == fieldVisibles.Length)
                    {
                        for (int i = 0; i < fieldindexes.Length; i++)
                        {
                            FieldDescribe fieldDescribe = new FieldDescribe();
                            fieldDescribe.Index = Convert.ToInt32(fieldindexes[i]);
                            fieldDescribe.Name = fieldNames[i];
                            fieldDescribe.Title = fieldTitles[i];
                            fieldDescribe.Length = Convert.ToInt32(fieldLengths[i]);
                            fieldDescribe.Visible = Convert.ToBoolean(fieldVisibles[i]);
                        }
                    }
                }
            }
        }
        #endregion

        #region Select Xpobject SelectXpobjectToTable RaiseSelectXpobjectChanged
        private XPObject _selectXpobject;
        public XPObject SelectXpobject
        {
            get
            {
                return _selectXpobject;
            }
            set
            {
                SetSelectXpobject(value);
            }
        }

        private void SetSelectXpobject(XPObject value)
        {
            if (_selectXpobject != value)
            {
                if (SelectXpobjectToTable(value))
                {
                    XPObject oldXpobject = _selectXpobject;
                    XPObject newXpobject = value;
                    _selectXpobject = value;
                    OnSelectXpobjectChanged(oldXpobject, newXpobject);
                }
            }
        }

        private void OnSelectXpobjectChanged(XPObject oldXpobject, XPObject newXpobject)
        {
            if (SelectXpobjectChanged != null)
            {
                XpobjectChangedEventArgs e = new XpobjectChangedEventArgs(oldXpobject, newXpobject, XpobjectChangedReason.None);
                SelectXpobjectChanged(this, e);
            }
        }

        protected void RaiseSelectXpobjectChanged(XPObject newXpobject)
        {
            OnSelectXpobjectChanged(SelectXpobject, newXpobject);
        }

        protected abstract bool SelectXpobjectToTable(XPObject value);

        public event XpobjectChangedEventHandler SelectXpobjectChanged;

        #endregion

        #region Initialization Table CreateColumns CreateRow
        protected override bool ValueToComponentImpl(XPCollection value)
        {
            if (_fieldDescribes.Count <= 0)
            {
                AutoCreateFieldDescribes(value);
            }
            CreateColumns(FieldDescribes);
            int index = 0;
            foreach (XPObject xpobject in value)
            {
                CreateRow(xpobject, index);
                index++;
            }
            return true;
        }

        protected abstract void CreateColumns(List<FieldDescribe> fieldDescribes);

        protected abstract void CreateRow(XPObject xpobject, int index);

        private bool _autoCreatingFieldDescribes = false;
        private void AutoCreateFieldDescribes(XPCollection collection)
        {
            _autoCreatingFieldDescribes = true;
            _fieldDescribesIndex.Value = "";
            _fieldDescribesName.Value = "";
            _fieldDescribesTitle.Value = "";
            _fieldDescribesLength.Value = "";
            _fieldDescribesVisible.Value = "";

            _fieldDescribesIndex.Value = "0";
            _fieldDescribesName.Value = "NO";
            _fieldDescribesTitle.Value = "NO";
            _fieldDescribesLength.Value = "60";
            _fieldDescribesVisible.Value = true.ToString();

            int index = 1;
            foreach (XPMemberInfo info in collection.ObjectClassInfo.PersistentProperties)
            {
                FieldDescribe fieldDescribe = new FieldDescribe();
                fieldDescribe.Index = index;
                index++;
                fieldDescribe.Name = info.Name;
                fieldDescribe.Title = info.Name;
                fieldDescribe.Length = 100;
                fieldDescribe.Visible = true;

                _fieldDescribesIndex.Value = _fieldDescribesIndex.Value + ' ' + fieldDescribe.Index.ToString();
                _fieldDescribesName.Value = _fieldDescribesName.Value + ' ' + fieldDescribe.Name;
                _fieldDescribesTitle.Value = _fieldDescribesTitle.Value + ' ' + fieldDescribe.Title;
                _fieldDescribesLength.Value = _fieldDescribesLength.Value + ' ' + fieldDescribe.Length.ToString();
                _fieldDescribesVisible.Value = _fieldDescribesVisible.Value + ' ' + fieldDescribe.Visible.ToString();

                _fieldDescribes.Add(fieldDescribe);
            }

            _fieldDescribesIndex.Value = _fieldDescribesIndex.Value.Trim();
            _fieldDescribesName.Value = _fieldDescribesName.Value.Trim();
            _fieldDescribesTitle.Value = _fieldDescribesTitle.Value.Trim();
            _fieldDescribesLength.Value = _fieldDescribesLength.Value.Trim();
            _fieldDescribesVisible.Value = _fieldDescribesVisible.Value.Trim();

            _autoCreatingFieldDescribes = false;
        }

        #endregion

        protected override bool ComponentToValueImpl(out XPCollection value)
        {
            value = null;
            return false;
        }
    }
    */
}
