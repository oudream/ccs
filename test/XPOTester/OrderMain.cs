using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace XPOTester
{
	public class OrderMain : XPObject
	{
		public OrderMain()
			: base()
		{
		}

		public OrderMain(Session session)
			: base(session)
		{
		}

		public OrderMain(Session session, XPClassInfo classInfo)
			: base(session, classInfo)
		{
		}

		[Association("OrderMain-OrderDetails")]
		public XPCollection<OrderDetail> OrderDetails
		{
			get
			{
				return GetCollection<OrderDetail>("OrderDetails");
			}
		}

		public string ClientName { get; set; }
	}
}
