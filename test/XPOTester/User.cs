using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using System.Drawing;
using DevExpress.Xpo.Metadata;

namespace XPOTester
{
	public class User : People
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

		public string UserName { get; set; }

		public string PassWordA { get; set; }

		public string PassWordB { get; set; }

		[Delayed, Size(-1), ValueConverter(typeof(ImageValueConverter))]
		public Image Photo
		{
			get { return GetDelayedPropertyValue<System.Drawing.Image>("Photo"); }
			set { SetDelayedPropertyValue<System.Drawing.Image>("Photo", value); }
		}

		public Department Department { get; set; }

		public string Templet { get; set; }

		[Association(("User-Role"), typeof(Role))]
		public XPCollection Roles
		{
			get
			{
				return base.GetCollection("Roles");
			}
		}

		public string TempletA { get; set; }

		[Delayed, Size(-1)]
		public byte[] FingerPrintData{ get; set; }
	}
}
