using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;

namespace Hong.Xpo.UiModule
{
    public class ViewerEditor : ViewerBase
    {
        protected void OnSave(object sender, EventArgs e)
        {
            XPObject xpobject = this.CurrentXpobject;
            if (xpobject == null)
            {
                return;
            }
            Hong.Xpo.Module.XpobjectManager manager = XpobjectManager;
            if (manager == null)
            {
                return;
            }
            ViewInXpobject(xpobject);
            xpobject.Save();
        }

        private void ViewInXpobject(XPObject xpobject)
        {
            foreach (CellerBase celler in Cellers)
            {
                if (celler is UiControlObject)
                {
                    UiControlObject controlObject = celler as UiControlObject;
                    object value = controlObject.ValueBase;
                    if (value != null)
                    {
                        xpobject.SetMemberValue(controlObject.PropertyName.Value, value);
                    }
                }
            }
        }

        protected void OnSaveAs(object sender, EventArgs e)
        {
            Hong.Xpo.Module.XpobjectManager manager = XpobjectManager;
            if (manager == null)
            {
                return;
            }
            object obj = manager.Xpobjects.ObjectClassInfo.CreateObject(manager.Xpobjects.Session);
            if (!(obj is XPObject))
            {
                return;
            }
            XPObject xpobject = obj as XPObject;
            ViewInXpobject(xpobject);
            manager.Add(xpobject);
        }

        protected void OnRestore(object sender, EventArgs e)
        {
            XPObject xpobject = this.CurrentXpobject;
            if (xpobject == null)
            {
                return;
            }
            ViewOutXpobject(xpobject);
        }

        protected override void EventLink(UiControlObject control)
        {
            if (control is UiButton)
            {
                UiButton button = control as UiButton;
                if (button.EventName.Value.Equals(VariableValueDefine.Event_Save))
                {
                    button.Execute += new EventHandler(OnSave);
                }
                else if (button.EventName.Value.Equals(VariableValueDefine.Event_SaveAs))
                {
                    button.Execute += new EventHandler(OnSaveAs);
                }
                else if (button.EventName.Value.Equals(VariableValueDefine.Event_Restore))
                {
                    button.Execute += new EventHandler(OnRestore);
                }
            }
            else if (true)
            {
                
            }
        }

        protected override void SetCurrentXpobject(XPObject value)
        {
            base.SetCurrentXpobject(value);
            if (value == null)
            {
                return;
            }
            ViewOutXpobject(value);
        }

        private void ViewOutXpobject(XPObject xpobject)
        {
            foreach (CellerBase celler in Cellers)
            {
                if (celler is UiControlObject)
                {
                    UiControlObject controlObject = celler as UiControlObject;
                    if (controlObject.PropertyName.Value != null && controlObject.PropertyName.Value != "")
                    {
                        controlObject.ValueBase = xpobject.GetMemberValue(controlObject.PropertyName.Value);
                    }
                }
            }
        }
    }
}
