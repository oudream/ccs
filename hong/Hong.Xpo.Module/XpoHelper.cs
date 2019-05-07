using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;

namespace Hong.Xpo.Module
{
	public class XpoHelper
	{
		private static XpoHelper _instance;
		public static XpoHelper Singleton
		{
			get
			{
				if (_instance == null)
				{
					_instance = new XpoHelper();
				}
				return _instance;
			}
		}

		public XpoHelper()
		{
			_session = new Session();
			_session.Connection = DataBaseHelper.Singleton.Conn;
		}

		private Session _session;
		public Session Session
		{
			get
			{
				return _session;
			}
		}
	}
}
