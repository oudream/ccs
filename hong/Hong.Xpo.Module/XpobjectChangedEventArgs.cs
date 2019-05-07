using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;

namespace Hong.Xpo.Module
{
    public class XpobjectChangedEventArgs
    {
        private readonly XpobjectChangedReason _reason;
        private readonly XPObject _oldXpobject;
        private readonly XPObject _newXpobject;

        public XpobjectChangedEventArgs(XPObject oldXpobject, XPObject newXpobject, XpobjectChangedReason reason)
		{
            _oldXpobject = oldXpobject;
            _newXpobject = newXpobject;
            _reason = reason;
		}

        public XpobjectChangedReason Reason
		{
			get
			{
                return _reason;
			}
		}

        public XPObject OldXpobject
		{
			get
			{
				return _oldXpobject;
			}
		}

		public XPObject NewXpobject
		{
			get
			{
				return _newXpobject;
			}
		}
}
}
