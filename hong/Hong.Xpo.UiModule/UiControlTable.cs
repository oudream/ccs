using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using Hong.Xpo.Module;
using Hong.Profile.Base;
using DevExpress.Xpo.Metadata;

namespace Hong.Xpo.UiModule
{
    public abstract class UiControlTable : UiControlGeneric<XPCollection>
    {
        public UiControlTable()
        {
        }

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
            if (Viewer.XpobjectManager == null)
            {
                return false; ;
            }
            TableCreateColumns(Viewer.XpobjectManager.XpobjectFieldUIAttributes);
            int index = 0;
            foreach (XPObject xpobject in value)
            {
                TableCreateRow(xpobject, index);
                index++;
            }
            TableCreateEnd();
            return true;
        }

        protected abstract void TableCreateEnd();

        protected abstract void TableCreateColumns(List<XpobjectFieldUIAttribute> fieldUIAttributes);

        protected abstract void TableCreateRow(XPObject xpobject, int index);

        #endregion

        protected override bool ComponentToValueImpl(out XPCollection value)
        {
            value = null;
            return false;
        }
    }
}
