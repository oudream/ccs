using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using Hong.Xpo.Module;
using DevExpress.Xpo.Metadata;
using System.Drawing;

namespace Hong.ChildSafeSystem.Module
{
    [XpobjectClassUIAttribute(new string[] { "FullName" })]
    public class Member : XPObject
    {
        public Member()
            : base()
        {
        }

        public Member(Session session)
            : base(session)
        {
        }

        public Member(Session session, XPClassInfo classInfo)
            : base(session, classInfo)
        {
        }

        [XpobjectFieldUIAttribute(0, false, "Fill It")]
        public string FullName { get; set; }

        [XpobjectFieldUIAttribute(1, false, "Fill It")]
        public string Code { get; set; }

        [XpobjectFieldUIAttribute(2, false, Gender.Male)]
        public Gender Gender { get; set; }

        [XpobjectFieldUIAttribute(3, true, null)]
        [Delayed, Size(-1), ValueConverter(typeof(ImageValueConverter))]
        public Image Photo
        {
            get { return GetDelayedPropertyValue<System.Drawing.Image>("Photo"); }
            set { SetDelayedPropertyValue<System.Drawing.Image>("Photo", value); }
        }

        [XpobjectFieldUIAttribute(4, true, "")]
        public string Address { get; set; }

        [XpobjectFieldUIAttribute(5, true, "")]
        public string Telephone { get; set; }

        [XpobjectFieldUIAttribute(6, true, "")]
        public string Mobile { get; set; }

        [XpobjectFieldUIAttribute(7, true, -1)]
        public int Age { get; set; }

        [XpobjectFieldUIAttribute(8, true, -1)]
        public DateTime Birthday { get; set; }

        [XpobjectFieldUIAttribute(9, true, "")]
        public string IDCard { get; set; }

        [XpobjectFieldUIAttribute(10, true, "")]
        public string Remark { get; set; }

        protected static FingerSourceType _fingerSourceType;
        public FingerSourceType FingerSourceType
        {
            get
            {
                return _fingerSourceType;
            }
        }
    }
}
