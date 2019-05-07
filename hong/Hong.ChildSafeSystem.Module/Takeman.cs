using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;

namespace Hong.ChildSafeSystem.Module
{
    public class Takeman : XPObject
	{
		public Takeman()
			: base()
		{
		}

		public Takeman(Session session)
			: base(session)
		{
		}

		public Takeman(Session session, XPClassInfo classInfo)
			: base(session, classInfo)
		{
		}

		private Student _student;
		[Association("Student-Takemen", typeof(Student))]
		public Student Student
		{
			get
			{
				return _student;
			}
			set
			{
				SetPropertyValue("Student", ref _student, value);
			}
		}

		private Genearch _genearch;
		[Association("Genearch-Takemen", typeof(Genearch))]
		public Genearch Genearch
		{
			get
			{
				return _genearch;
			}
			set
			{
				SetPropertyValue("Genearch", ref _genearch, value);
			}
		}

		public string RelationName { get; set; }
	}
}
