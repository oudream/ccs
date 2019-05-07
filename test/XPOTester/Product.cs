using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace XPOTester
{
	public class Product : XPObject
	{
		public Product()
			: base()
		{
		}

		public Product(Session session)
			: base(session)
		{
		}

		public Product(Session session, XPClassInfo classInfo)
			: base(session, classInfo)
		{
		}

		public string Name { get; set; }
	}
}
