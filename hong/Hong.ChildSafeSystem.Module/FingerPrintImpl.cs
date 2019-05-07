using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.ChildSafeSystem.Module
{
	public class FingerPrintImpl
	{
		public FingerPrintImpl()
		{
			FingerId = -1;
			TemplateSize = 0;
			Template = new byte[0];
		}

		public int FingerId { get; set; }

		public FingerSourceType FingerSourceType { get; set; }

		public int PeopleId { get; set; }

		public FingerType FingerType { get; set; }

		public int TemplateSize { get; set; }

		public byte[] Template { get; set; }
	}
}
