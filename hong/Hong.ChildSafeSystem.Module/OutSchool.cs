using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using Hong.Xpo.Module;

namespace Hong.ChildSafeSystem.Module
{
	public class OutSchool : XPObject
	{
		public OutSchool()
			: base()
		{
		}

		public OutSchool(Session session)
			: base(session)
		{
		}

		public OutSchool(Session session, XPClassInfo classInfo)
			: base(session, classInfo)
		{
		}

        public static OutSchool CreateInstance()
        {
            OutSchool outschool = new OutSchool(XpoHelper.Singleton.Session);
            return outschool;
        }
        
        public Genearch Genearch { get; set; }

		public Student Student { get; set; }

		public DateTime OutSchoolTime { get; set; }
	}
}
