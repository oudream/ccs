using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using Hong.Xpo.Module;

namespace Hong.Xpo.UiModule
{
    public class ViewerLooker : ViewerBase
    {
        protected void OnRefresh(object sender, EventArgs e)
        {
        }

        protected void OnShutdown(object sender, EventArgs e)
        { 
        }

        protected void OnDelete(object sender, EventArgs e)
        { 
        }

        protected override void EventLink(UiControlObject control)
        {
            if (control is UiButton)
            {
                UiButton button = control as UiButton;
                if (button.EventName.Value.Equals(VariableValueDefine.Event_Refresh))
                {
                    button.Execute += new EventHandler(OnRefresh);
                }
                else if (button.EventName.Value.Equals(VariableValueDefine.Event_Delete))
                {
                    button.Execute += new EventHandler(OnDelete);
                }
                else if (button.EventName.Value.Equals(VariableValueDefine.Event_Shutdown))
                {
                    button.Execute += new EventHandler(OnShutdown);
                }
            }
            else if (control is UiControlTable)
            {
                UiControlTable table = control as UiControlTable;
                table.SelectXpobjectChanged += new Hong.Xpo.Module.XpobjectChangedEventHandler(table_SelectXpobjectChanged);
            }
        }

        void table_SelectXpobjectChanged(object sender, Hong.Xpo.Module.XpobjectChangedEventArgs e)
        {
            OnSelectXpobjectChanged(e.OldXpobject, e.NewXpobject);
        }

        private void OnSelectXpobjectChanged(XPObject oldXpobject, XPObject newXpobject)
        {
            if (SelectXpobjectChanged != null)
            {
                XpobjectChangedEventArgs e = new XpobjectChangedEventArgs(oldXpobject, newXpobject, XpobjectChangedReason.None);
                SelectXpobjectChanged(this, e);
            }
        }

        public event XpobjectChangedEventHandler SelectXpobjectChanged;

        protected override void SetCurrentXpobject(XPObject value)
        {
            base.SetCurrentXpobject(value);
            CurrentXpobjectToTable(value);
        }

        private void CurrentXpobjectToTable(XPObject value)
        {
            foreach (CellerBase celler in Cellers)
            {
                if (celler is UiControlTable)
                {
                    UiControlTable table = celler as UiControlTable;
                    if (String.IsNullOrEmpty(table.PropertyName.Value))
                    {
                        table.SelectXpobject = value;
                    }
                }
            }
        }

        protected override void SetXpobjectManager(XpobjectManager value)
        {
            base.SetXpobjectManager(value);
            foreach (CellerBase celler in Cellers)
            {
                if (celler is UiControlTable)
                {
                    UiControlTable table = celler as UiControlTable;
                    if (String.IsNullOrEmpty(table.PropertyName.Value))
                    {
                        table.Value = value.Xpobjects;
                    }
                }
            }
        }
    }
}
