using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using Hong.Xpo.Module;

namespace Hong.Xpo.UiModule
{
    public abstract class UiControlList : UiControlGeneric<XPObject>
    {
        public override void Initialization(System.Type valueType)
        {
            base.Initialization(valueType);
            XpobjectManager manager = XpobjectCenter.Singleton.GetManager(valueType);
            if (manager != null && XpobjectManager != manager)
            {
                LoadControlList(manager);
                XpobjectManager = manager;
            }
        }

        private XpobjectManager XpobjectManager;
        protected override bool ValueToComponentImpl(XPObject value)
        {
            if (value != null)
            {
                XpobjectManager manager = XpobjectCenter.Singleton.GetManager(value.GetType());
                if (manager != null && XpobjectManager != manager)
                {
                    LoadControlList(manager);
                    XpobjectManager = manager;
                }
            }
            XpobjectToControlList(value);
            return true;
        }

        protected abstract void XpobjectToControlList(XPObject value);

        private void LoadControlList(XpobjectManager manager)
        {
            ClearControlList();
            int index = 0;
            foreach (XPObject xpobject in manager.Xpobjects)
            {
                XpobjectItem xpobjectItem = new XpobjectItem(xpobject, manager.XpobjectClassUIAttribute.TitlePropertyNames);
                CreateControlItem(xpobjectItem, index);
                index++;
            }
        }

        protected abstract void CreateControlItem(XpobjectItem xpobjectItem, int index);

        protected abstract void ClearControlList();
    }
}
