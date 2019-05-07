using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using Hong.Xpo.Module;

namespace Hong.ChildSafeSystem.Module
{
    public class Student : Member
    {
        public Student()
            : base()
        {
        }

        public Student(Session session)
            : base(session)
        {
        }

        public Student(Session session, XPClassInfo classInfo)
            : base(session, classInfo)
        {
        }

        public static Student CreateInstance()
        {
            Student student = new Student(XpoHelper.Singleton.Session);
            return student;
        }

        [XpobjectFieldUIAttribute(3, true, null)]
        public Team Team { get; set; }

        [Association("Student-Takemen", typeof(Takeman))]
        public XPCollection Takemen
        {
            get
            {
                return base.GetCollection("Takemen");
            }
        }

        static Student()
        {
            _fingerSourceType = Hong.ChildSafeSystem.Module.FingerSourceType.Student;
        }

        public string TeamName()
        {
            if (Team != null)
            {
                return Team.Name;
            }
            else
            {
                return "";
            }
        }
    }
}
