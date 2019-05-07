using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace XPOTester
{
	public class Department : XPObject
	{
		public Department()
			: base()
		{
		}

		public Department(Session session) 
			: base(session)
		{
		}

		public Department(Session session, XPClassInfo classInfo)
			: base(session, classInfo)
		{ 
		}

	public string DepartmentName { get; set; }
	}
}
