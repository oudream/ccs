using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using Hong.Xpo.Module;

namespace Hong.ChildSafeSystem.Module
{
    [XpobjectClassUIAttribute(new string[] { "Name" })]
    public class Team : XPObject
	{
		public Team()
			: base()
		{
		}

		public Team(Session session)
			: base(session)
		{
		}

		public Team(Session session, XPClassInfo classInfo)
			: base(session, classInfo)
		{
		}

		public static Team CreateInstance()
		{
			Team team = new Team(XpoHelper.Singleton.Session);
			return team;
		}

		public string Name { get; set; }

		public string Title { get; set; }

		public Teacher Master { get; set; }
	}
}
