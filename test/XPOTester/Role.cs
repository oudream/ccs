using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace XPOTester
{
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

		[Association("User-Role")]
		public XPCollection<User> Users
		{
			get
			{
				return base.GetCollection<User>("Users");
                if (true)
                {

                }
                else if (true)
                {

                }
                else
                {

                }
            }
		}
	}
}
