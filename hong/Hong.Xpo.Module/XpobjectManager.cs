using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace Hong.Xpo.Module
{
    public class XpobjectManager
    {
        public XpobjectManager(Type type)
        {
            _xpobjectType = type;
            _xpobjects = CreateXPObjects(type);
            _xpobjectClassUIAttribute = CreateXpobjectClassUIAttribute(type);
            _xpobjectFieldUIAttributes = CreateXpobjectFieldUIAttributes(_xpobjects);
        }

        private XpobjectClassUIAttribute CreateXpobjectClassUIAttribute(Type type)
        {
            XpobjectClassUIAttribute attribute = new XpobjectClassUIAttribute();
            object[] objs = type.GetCustomAttributes(true);
            foreach (object obj in objs)
            {
                if (obj is XpobjectClassUIAttribute)
                {
                    string[] names = (obj as XpobjectClassUIAttribute).TitlePropertyNames;
                    attribute.SetTitlePropertyNames(names);
                }
            }
            attribute.SetTitle(type.Name);
            return attribute;
        }

        private XpobjectClassUIAttribute _xpobjectClassUIAttribute;
        public XpobjectClassUIAttribute XpobjectClassUIAttribute
        {
            get
            {
                return _xpobjectClassUIAttribute;
            }
        }

        private List<XpobjectFieldUIAttribute> CreateXpobjectFieldUIAttributes(XPCollection collection)
        {
            List<XpobjectFieldUIAttribute> attributes = new List<XpobjectFieldUIAttribute>();
            int ordernumber = 0;
            foreach (XPMemberInfo info in collection.ObjectClassInfo.PersistentProperties)
            {
                XpobjectFieldUIAttribute attribute = null;
                object[] objs = info.Attributes;
                foreach (object obj in objs)
                {
                    if (obj is XpobjectFieldUIAttribute)
                    {
                        attribute = new XpobjectFieldUIAttribute();
                        attribute.AssignFrom(obj as XpobjectFieldUIAttribute);
                        if (attribute.Ordernumber >= ordernumber)
                        {
                            ordernumber = attribute.Ordernumber + 1;
                        }
                        break;
                    }
                }
                if (attribute == null)
                {
                    attribute = new XpobjectFieldUIAttribute(ordernumber, true, null);
                }
                attribute.SetFieldName(info.Name);
                attribute.SetFieldType(info.MemberType);
                attribute.SetFieldTitle(info.Name);
                if (Type.Equals(info.MemberType, typeof(int)))
                {
                    attribute.SetFieldWidth(50);
                }
                else if (Type.Equals(info.MemberType, typeof(string)))
                {
                    attribute.SetFieldWidth(60);
                }
                else
                {
                    attribute.SetFieldWidth(100);
                }
                if (XpobjectCenter.Singleton.IsFixMember(info.Name))
                {
                    attribute.SetVisible(false);
                }
                else
                {
                    attribute.SetVisible(true);
                }
                attributes.Add(attribute);
            }
            attributes.Sort(ComparisonXpobjectFieldUIAttribute);
            return attributes;
        }

        private int ComparisonXpobjectFieldUIAttribute(XpobjectFieldUIAttribute x, XpobjectFieldUIAttribute y)
        {
            if (x.Ordernumber > y.Ordernumber)
            {
                return 1;
            }
            else if (x.Ordernumber == y.Ordernumber)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        private List<XpobjectFieldUIAttribute> _xpobjectFieldUIAttributes;
        public List<XpobjectFieldUIAttribute> XpobjectFieldUIAttributes
        {
            get
            {
                return _xpobjectFieldUIAttributes;
            }
        }

        protected virtual XPCollection CreateXPObjects(Type objType)
        {
            return new XPCollection(XpoHelper.Singleton.Session, objType);
        }

        private Type _xpobjectType;
        public Type XpobjectType
        {
            get
            {
                return _xpobjectType;
            }
        }

        private XPCollection _xpobjects;
        public XPCollection Xpobjects
        {
            get
            {
                return _xpobjects;
            }
            set
            {
                _xpobjects = value;
            }
        }

        public string XpobjectFullName
        {
            get
            {
                return _xpobjectType.FullName;
            }
        }

        public XPObject GetXpobject(int oid)
        {
            foreach (XPObject item in _xpobjects)
            {
                if (item.Oid.Equals(oid))
                {
                    return item;
                }
            }
            return null;
        }

        #region Xpobject Operation
        public int Add(XPObject xpobject)
        {
            xpobject.Save();
            _xpobjects.Add(xpobject);
            return xpobject.Oid;
        }

        public int Delete(XPObject xpobject)
        {
            if (xpobject != null)
            {
                xpobject.Delete();
            }
            return 1;
        }

        public int Delete(int oid)
        {
            XPObject xpobject = GetXpobject(oid);
            if (xpobject != null)
            {
                xpobject.Delete();
            }
            return 1;
        }

        public void Reload()
        {
            _xpobjects.Reload();
        }
        #endregion
    }
}
