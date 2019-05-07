using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.ChildSafeSystem.Module
{
	public enum Gender
	{
		Male,
		Female
	}


	public static class GenderManager
	{
		static GenderManager()
		{
		}

		public const string DisplayMale = "男";

		public const string DisplayFemale = "女";

		public const string DisplayDefaul = "男";

		public static Gender GenderByDislay(string display)
		{
			if (DisplayMale.Equals(display))
			{
				return Gender.Male;
			}
			else
			{
				return Gender.Female;
			}
		}

		public static string DislayByGender(Gender gender)
		{
			if (gender == Gender.Male)
			{
				return DisplayMale;
			}
			else
			{
				return DisplayFemale;
			}
		}

		public static string[] Displays()
		{
			string[] displays = new string[2] { DisplayMale, DisplayFemale };
			return displays;
		}
	}
}
