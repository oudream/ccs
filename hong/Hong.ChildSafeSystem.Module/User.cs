using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace Hong.ChildSafeSystem.Module
{
    public class User : Member
	{
		public User()
			: base()
		{
		}

		public User(Session session)
			: base(session)
		{
		}

		public User(Session session, XPClassInfo classInfo)
			: base(session, classInfo)
		{
		}

		public string Username { get; set; }

		public string Password { get; set; }

		[Association(("User-Role"), typeof(Role))]
		public XPCollection Roles
		{
			get
			{
				return base.GetCollection("Roles");
			}
		}

        public bool IsRole(Role role)
        {
            foreach (Role item in Roles)
            {
                if (item.Equals(role))
                {
                    return true;
                }
            }
            return false;
        }
	}
}
