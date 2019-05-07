using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo.Metadata;
using DevExpress.Xpo;
using System.Drawing;
using Hong.Xpo.Module;

namespace Hong.ChildSafeSystem.Module
{
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

		public string MiddleName { get; set; }

		public string LastName { get; set; }

		public string FullName { get; set; }

		public string DisplayName { get; set; }

		public string Code { get; set; }

		public string IDCard { get; set; }

		public Gender Gender { get; set; }

		public Contact Contact { get; set; }

		public DateTime Birthday { get; set; }

		public int Age { get; set; }

		[Delayed, Size(-1), ValueConverter(typeof(ImageValueConverter))]
		public Image Photo
		{
			get { return GetDelayedPropertyValue<System.Drawing.Image>("Photo"); }
			set { SetDelayedPropertyValue<System.Drawing.Image>("Photo", value); }
		}
    }
}
