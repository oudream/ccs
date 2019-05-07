using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo.Metadata;
using DevExpress.Xpo;
using Hong.Xpo.Module;

namespace Hong.ChildSafeSystem.Module
{
    public class Teacher : Member
	{
		public Teacher()
			: base()
		{
		}

		public Teacher(Session session)
			: base(session)
		{
		}

		public Teacher(Session session, XPClassInfo classInfo)
			: base(session, classInfo)
		{
		}

		public static Teacher CreateInstance()
		{
			Teacher teacher = new Teacher(XpoHelper.Singleton.Session);
			return teacher;
		}

		public Position Position { get; set; }

        static Teacher()
        {
            _fingerSourceType = Hong.ChildSafeSystem.Module.FingerSourceType.Teacher;
        }
	}
}
