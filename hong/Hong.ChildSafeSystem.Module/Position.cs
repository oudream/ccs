using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using Hong.Xpo.Module;

namespace Hong.ChildSafeSystem.Module
{
    [XpobjectClassUIAttribute(new string[] {"Name"})]
    public class Position : XPObject
	{
		public Position()
			: base()
		{
		}

		public Position(Session session)
			: base(session)
		{
		}

		public Position(Session session, XPClassInfo classInfo)
			: base(session, classInfo)
		{
		}

		public string Name { get; set; }

		public string Title { get; set; }

		public static Position CreateInstance()
		{
			return new Position(XpoHelper.Singleton.Session);
		}
	}
}
