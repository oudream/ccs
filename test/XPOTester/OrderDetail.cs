using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace XPOTester
{
	public class OrderDetail : XPObject
	{
		public OrderDetail()
			: base()
		{
		}

		public OrderDetail(Session session)
			: base(session)
		{
		}

		public OrderDetail(Session session, XPClassInfo classInfo)
			: base(session, classInfo)
		{
		}

		private OrderMain _orderMain;
		[Association("OrderMain-OrderDetails", typeof(OrderMain))]
		public OrderMain OrderMain
		{
			get
			{
				return _orderMain;
			}
			set
			{
				SetPropertyValue("OrderMain", ref _orderMain, value);
			}
		}

		public Product Product { get; set; }

		public int Price { get; set; }

		public int Count { get; set; }
	}
}
