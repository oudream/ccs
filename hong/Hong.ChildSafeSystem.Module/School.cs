using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using Hong.Xpo.Module;

namespace Hong.ChildSafeSystem.Module
{
    [XpobjectClassUIAttribute(new string[] { "Name" })]
    public class School : XPObject
    {
        public School()
            : base()
        {
        }

        public School(Session session)
            : base(session)
        {
        }

        public School(Session session, XPClassInfo classInfo)
            : base(session, classInfo)
        {
        }

        public static School CreateInstance(Session session, XPClassInfo classInfo)
        {
            return new School(XpoHelper.Singleton.Session);
        }

        public string Name { get; set; }

        public string DatabaseName { get; set; }
    }
}
