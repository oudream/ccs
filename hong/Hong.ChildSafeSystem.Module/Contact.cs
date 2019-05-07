using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using Hong.Xpo.Module;

namespace Hong.ChildSafeSystem.Module
{
    [XpobjectClassUIAttribute(new string[] {"Mobile", "Telephone", "Email", "Address"})]
    public class Contact : XPObject
	{
		public Contact()
			: base()
		{
		}

		public Contact(Session session)
			: base(session)
		{
		}

		public Contact(Session session, XPClassInfo classInfo)
			: base(session, classInfo)
		{
		}

		public static Contact CreateInstance()
		{
			return new Contact(XpoHelper.Singleton.Session);
		}

		private string _country;
		public string Country
		{
			get
			{
				return _country;
			}
			set
			{
				SetPropertyValue("Country", ref _country, value);
			}
		}

		private string _province;
		public string Province
		{
			get
			{
				return _province;
			}
			set
			{
				SetPropertyValue("Province", ref _province, value);
			}
		}

		private string _area;
		public string Area
		{
			get
			{
				return _area;
			}
			set
			{
				SetPropertyValue("Area", ref _area, value);
			}
		}

		private string _postalcode;
		public string Postalcode
		{
			get
			{
				return _postalcode;
			}
			set
			{
				SetPropertyValue("Postalcode", ref _postalcode, value);
			}
		}

		private string _address;
		public string Address
		{
			get
			{
				return _address;
			}
			set
			{
				SetPropertyValue("Address", ref _address, value);
			}
		}

		private string _address1;
		public string Address1
		{
			get
			{
				return _address1;
			}
			set
			{
				SetPropertyValue("Address1", ref _address1, value);
			}
		}

		private string _address2;
		public string Address2
		{
			get
			{
				return _address2;
			}
			set
			{
				SetPropertyValue("Address2", ref _address2, value);
			}
		}

		private string _telephone;
		public string Telephone
		{
			get
			{
				return _telephone;
			}
			set
			{
				SetPropertyValue("Telephone", ref _telephone, value);
			}
		}

		private string _telephone1;
		public string Telephone1
		{
			get
			{
				return _telephone1;
			}
			set
			{
				SetPropertyValue("Telephone1", ref _telephone1, value);
			}
		}

		private string _telephone2;
		public string Telephone2
		{
			get
			{
				return _telephone2;
			}
			set
			{
				SetPropertyValue("Telephone2", ref _telephone2, value);
			}
		}

		private string _mobile;
		public string Mobile
		{
			get
			{
				return _mobile;
			}
			set
			{
				SetPropertyValue("Mobile", ref _mobile, value);
			}
		}

		private string _mobile1;
		public string Mobile1
		{
			get
			{
				return _mobile1;
			}
			set
			{
				SetPropertyValue("Mobile1", ref _mobile1, value);
			}
		}

		private string _mobile2;
		public string Mobile2
		{
			get
			{
				return _mobile2;
			}
			set
			{
				SetPropertyValue("Mobile2", ref _mobile2, value);
			}
		}

		private string _msn;
		public string MSN
		{
			get
			{
				return _msn;
			}
			set
			{
				SetPropertyValue("MSN", ref _msn, value);
			}
		}

		private string _qq;
		public string QQ
		{
			get
			{
				return _qq;
			}
			set
			{
				SetPropertyValue("QQ", ref _qq, value);
			}
		}

		private string _email;
		public string Email
		{
			get
			{
				return _email;
			}
			set
			{
				SetPropertyValue("Email", ref _email, value);
			}
		}

		private string _company;
		public string Company
		{
			get
			{
				return _company;
			}
			set
			{
				SetPropertyValue("Company", ref _company, value);
			}
		}

		private string _companyAddress;
		public string CompanyAddress
		{
			get
			{
				return _companyAddress;
			}
			set
			{
				SetPropertyValue("CompanyAddress", ref _companyAddress, value);
			}
		}

		private string _companyTelephone;
		public string CompanyTelephone
		{
			get
			{
				return _companyTelephone;
			}
			set
			{
				SetPropertyValue("CompanyTelephone", ref _companyTelephone, value);
			}
		}
	}
}
