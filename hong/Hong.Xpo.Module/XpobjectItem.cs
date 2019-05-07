using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;

namespace Hong.Xpo.Module
{
    public class XpobjectItem
    {
        public XpobjectItem(XPObject xpobject, string[] titlePropertyNames)
        {
            _xpobject = xpobject;
            _titlePropertyNames = titlePropertyNames;
        }

        private XPObject _xpobject;
        public XPObject XPObject
        {
            get
            {
                return _xpobject;
            }
        }

        private string[] _titlePropertyNames;
        public string[] TitlePropertyNames
        {
            get
            {
                return _titlePropertyNames;
            }
        }

        public override string ToString()
        {
            if (TitlePropertyNames != null)
            {
                string value = "";
                foreach (string propertyName in TitlePropertyNames)
                {
                    object obj = _xpobject.GetMemberValue(propertyName);
                    if (obj != null)
                    {
                        value += obj.ToString() + " ";
                    }
                }
                return value;
            }
            else
            {
                return _xpobject.GetMemberValue("Oid").ToString();
            }
        }
    }
}
