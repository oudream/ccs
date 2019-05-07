using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using System.Reflection;
using DevExpress.Xpo.Metadata;

namespace Hong.Xpo.Module
{
    public class XpobjectCenter
    {
        public XpobjectCenter()
        {
            _managers = new List<XpobjectManager>();
        }

        private static XpobjectCenter _instance;
        public static XpobjectCenter Singleton
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new XpobjectCenter();
                }
                return _instance;
            }
        }

        private List<XpobjectManager> _managers;
        public List<XpobjectManager> Managers
        {
            get
            {
                return _managers;
            }
        }

        public XpobjectManager GetManager(string fullName)
        {
            foreach (XpobjectManager item in _managers)
            {
                if (item.XpobjectFullName == fullName)
                {
                    return item;
                }
            }

            return null;
        }

        public XpobjectManager GetManager(Type type)
        {
            foreach (XpobjectManager item in _managers)
            {
                if (item.XpobjectType == type)
                {
                    return item;
                }
            }

            if (type.IsSubclassOf(typeof(XPObject)))
            {
                XpobjectManager manager = new XpobjectManager(type);
                _managers.Add(manager);
                return manager;
            }

            return null;
        }

        public void RegisterManager(XpobjectManager manager)
        {
            if (_managers.IndexOf(manager) < 0)
            {
                _managers.Add(manager);
            }
        }

        public bool IsFixMember(string name)
        {
            if (name.Equals(XpobjectConstDefine.PropertyNameOid) || name.Equals(XpobjectConstDefine.PropertyNameGCRecord) || name.Equals(XpobjectConstDefine.PropertyNameOptimisticLockField) || name.Equals(XpobjectConstDefine.PropertyNameObjectType))
            {
                return true;
            }
            return false;
        }
    }
}
