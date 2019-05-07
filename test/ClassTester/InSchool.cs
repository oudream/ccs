using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace ClassTester
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

		public string Student { get; set; }

		public DateTime InSchoolTime { get; set; }

        public static string ClassTest()
        {
            return "InSchool";
        }
	}
}
