using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace XPOTester
{
	public enum Gender
	{
		Male,
		Female
	}

	public class People : XPObject
	{
		public People() 
			: base()
		{
		}

		public People(Session session) 
			: base(session)
		{
		}

		public People(Session session, XPClassInfo classInfo)
			: base(session, classInfo)
		{ 
		}

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string Name { get; set; }

		public Gender Gender { get; set; }
	}
}
