using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using Hong.Xpo.Module;

namespace Hong.ChildSafeSystem.Module
{
	public class InSchool : XPObject
	{
		public InSchool()
			: base()
		{
		}

		public InSchool(Session session)
			: base(session)
		{
		}

		public InSchool(Session session, XPClassInfo classInfo)
			: base(session, classInfo)
		{
		}

        public static InSchool CreateInstance()
        {
            InSchool inschool = new InSchool(XpoHelper.Singleton.Session);
            return inschool;
        }

        public Student Student { get; set; }

		public DateTime InSchoolTime { get; set; }
    }
}
