using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using Hong.Xpo.Module;

namespace Hong.ChildSafeSystem.Module
{
    [XpobjectClassUIAttribute(new string[] { "Name" })]
    public class Role : XPObject
	{
		public Role()
			: base()
		{
		}

		public Role(Session session)
			: base(session)
		{
		}

		public Role(Session session, XPClassInfo classInfo)
			: base(session, classInfo)
		{
		}

		public string Name { get; set; }

		public string DisplayName { get; set; }

		[Association("User-Role", typeof(User))]
		public XPCollection Users
		{
			get
			{
				return base.GetCollection("Users");
			}
		}
	}
}
