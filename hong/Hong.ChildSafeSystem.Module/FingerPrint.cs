
using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using Hong.Xpo.Module;

namespace Hong.ChildSafeSystem.Module
{
	public class FingerPrint : XPObject
	{
		public FingerPrint()
			: base()
		{
			_fingerPrintImpl = new FingerPrintImpl();
		}

		public FingerPrint(Session session)
			: base(session)
		{
			_fingerPrintImpl = new FingerPrintImpl();
		}

		public FingerPrint(Session session, XPClassInfo classInfo)
			: base(session, classInfo)
		{
			_fingerPrintImpl = new FingerPrintImpl();
		}

		public static FingerPrint CreateInstance(FingerPrintImpl fingerPrintImpl)
		{
			FingerPrint fingerPrint = new FingerPrint(XpoHelper.Singleton.Session);
			fingerPrint.FingerPrintImpl = fingerPrintImpl;
			return fingerPrint;
		}

		public static FingerPrint CreateInstance()
		{
			FingerPrint fingerPrint = new FingerPrint(XpoHelper.Singleton.Session);
			return fingerPrint;
		}

		private FingerPrintImpl _fingerPrintImpl;
		public FingerPrintImpl FingerPrintImpl
		{
			get
			{
				_fingerPrintImpl.FingerId = this.Oid;
				return _fingerPrintImpl;
			}
			set
			{
				//Oid = value.FingerId;
				_fingerPrintImpl = value;
			}
		}

		public FingerSourceType FingerSourceType
		{
			get
			{
				return _fingerPrintImpl.FingerSourceType;
			}
			set
			{
				_fingerPrintImpl.FingerSourceType = value;
			}
		}

		public int PeopleId
		{
			get
			{
				return _fingerPrintImpl.PeopleId;
			}
			set
			{
				_fingerPrintImpl.PeopleId = value;
			}
		}

		public FingerType FingerType
		{
			get
			{
				return _fingerPrintImpl.FingerType;
			}
			set
			{
				_fingerPrintImpl.FingerType = value;
			}
		}

		public int TemplateSize
		{
			get
			{
				return _fingerPrintImpl.TemplateSize;
			}
			set
			{
				_fingerPrintImpl.TemplateSize = value;
			}
		}

		public byte[] Template
		{
			get
			{
				return _fingerPrintImpl.Template;
			}
			set
			{
				_fingerPrintImpl.Template = value;
			}
		}
	}
}
