using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Common.Stringer
{
 	public class StringHelper
	{
		public static readonly char[] WhitespaceChars;

		static StringHelper()
		{
			WhitespaceChars = new char[] { 
				'\t', '\n', '\v', '\f', '\r', ' ', '\x0085', '\x00a0', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 
				' ', ' ', ' ', ' ', '​', '\u2028', '\u2029', '　', '﻿'
			};
		}

		public static bool IsWhitespaceChar(char c)
		{
			for (int i = 0; i < WhitespaceChars.Length; i++)
			{
				if (WhitespaceChars[i] == c)
				{
					return true;
				}
			}
			return false;
		}
	}
}
