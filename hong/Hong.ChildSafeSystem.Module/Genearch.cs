using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using Hong.Xpo.Module;

namespace Hong.ChildSafeSystem.Module
{
    public class Genearch : Member
    {
        public Genearch()
            : base()
        {
        }

        public Genearch(Session session)
            : base(session)
        {
        }

        public Genearch(Session session, XPClassInfo classInfo)
            : base(session, classInfo)
        {
        }

        public static Genearch CreateInstance()
        {
            Genearch genearch = new Genearch(XpoHelper.Singleton.Session);
            return genearch;
        }

        [Association("Genearch-Takemen", typeof(Takeman))]
        public XPCollection Takemen
        {
            get
            {
                return base.GetCollection("Takemen");
            }
        }

        static Genearch()
        {
            _fingerSourceType = Hong.ChildSafeSystem.Module.FingerSourceType.Genearch;
        }
 }
}
