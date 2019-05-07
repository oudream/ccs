using System;
using System.Collections.Generic;
using System.Text;

namespace Hong.Common.Stringer
{
	public class StringHexer
	{
		protected static string CovertToStringHex(byte[] buf)
		{
			if (buf == null || buf.Length <= 0)
				return "";
			StringBuilder sb = new StringBuilder(buf.Length * 3);
			foreach (byte item in buf)
			{
				sb.AppendFormat("{0:X2} ", item);
			}
			return sb.ToString();
		}

		protected static string CovertToStringHex(byte[] buf, int index, int count)
		{
			if (!IsValidBuffer(buf, index, count))
			{
				return "";
			}
			StringBuilder sb = new StringBuilder(buf.Length * 3);
			for (int i = 0; i < count; i++)
			{
				sb.AppendFormat("{0:X2} ", buf[index + i]);
			}
			return sb.ToString();
		}

		protected static byte[] CovertToBuffer(string sHex)
		{
			if (sHex == null)
			{
				return new byte[0];
			}
			sHex = sHex.Replace(" ", "");
			if (sHex.Length <= 0)
			{
				return new byte[0];
			}
			try
			{
				List<byte> data = new List<byte>();
				for (int i = 0; i < sHex.Length; i += 2)
				{
					data.Add(byte.Parse(sHex.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));
				}
				return data.ToArray();
			}
			catch (Exception)
			{
				return new byte[0];
			}
		}

		public static bool IsValidBuffer(byte[] buf, int index, int count)
		{
			if (buf == null || buf.Length <= 0)
			{
				return false;
			}
			if (!((index >= 0 && index < buf.Length) && (count <= buf.Length - index)))
			{
				return false;
			}
			return true;
		}

		public static bool IsValidHexString(string hexString)
		{
			if (hexString == null)
			{
				return false;
			}
			int index = 0;
			char c;
			while (index < hexString.Length)
			{
				c = hexString[index++];
				if (! (IsValidHexChar(c) || StringHelper.IsWhitespaceChar(c)))
				{
					return false;
				}
			}
			return true;
		}

		public static bool IsValidHexChar(char c)
		{
			if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f') || (c >= 'A' && c <= 'F'))
			{
				return true;
			}
			return false;
		}

		#region 十六进制字符串转换
		/// <summary>
		/// 异常：如果参数 val 不对
		/// </summary>
		/// <param name="val"></param>
		/// <returns></returns>
		public static int ConvertHexDigit(char val)
		{
			if ((val <= '9') && (val >= '0'))
			{
				return (val - '0');
			}
			if ((val >= 'a') && (val <= 'f'))
			{
				return ((val - 'a') + 10);
			}
			if ((val < 'A') || (val > 'F'))
			{
				throw new ArgumentException("ArgumentOutOfRange_Index");
			}
			return ((val - 'A') + 10);
		}

		public static byte[] DecodeHexString(string hexString)
		{
			if (hexString == null || hexString.Length <= 0)
			{
				return new byte[0];
			}
			char[] hexCharArray = new char[hexString.Length];
			int len = 0;
			for (int i = 0; i < hexString.Length; i++)
			{
				if (IsValidHexChar( hexString[i] ))
				{
					hexCharArray[len++] = hexString[i];
				}
			}
			if (len % 2 != 0)
			{
				return new byte[0];
			}
			byte[] buffer = new byte[len / 2];
			int num = 0;
			try
			{
				for (int i = 0; num < len; i++)
				{
					int num4 = ConvertHexDigit(hexCharArray[num]);
					int num3 = ConvertHexDigit(hexCharArray[num + 1]);
					buffer[i] = (byte)(num3 | (num4 << 4));
					num += 2;
				}
				return buffer;
			}
			catch (Exception)
			{
			}
			return new byte[0];
			//以下是原始代码
			/*
			byte[] buffer;
			if (hexString == null)
			{
				throw new ArgumentNullException("hexString");
			}
			bool flag = false;
			int num = 0;
			int length = hexString.Length;
			if (((length >= 2) && (hexString[0] == '0')) && ((hexString[1] == 'x') || (hexString[1] == 'X')))
			{
				length = hexString.Length - 2;
				num = 2;
			}
			if (((length % 2) != 0) && ((length % 3) != 2))
			{
				throw new ArgumentException(Environment.GetResourceString("Argument_InvalidHexFormat"));
			}
			if ((length >= 3) && (hexString[num + 2] == ' '))
			{
				flag = true;
				buffer = new byte[(length / 3) + 1];
			}
			else
			{
				buffer = new byte[length / 2];
			}
			for (int i = 0; num < hexString.Length; i++)
			{
				int num4 = ConvertHexDigit(hexString[num]);
				int num3 = ConvertHexDigit(hexString[num + 1]);
				buffer[i] = (byte)(num3 | (num4 << 4));
				if (flag)
				{
					num++;
				}
				num += 2;
			}
			return buffer;
			*/
		}

		public static string EncodeHexString(byte[] buf, int index, int count)
		{
			if (!IsValidBuffer(buf, index, count))
			{
				return "";
			}
			char[] chArray = new char[count * 3];
			int index3 = index;
			int num3 = 0;
			while (index3 < (index + count))
			{
				int num = (buf[index3] & 0xF0) >> 4;
				chArray[num3++] = HexDigit(num);
				num = buf[index3] & 0x0F;
				chArray[num3++] = HexDigit(num);
				chArray[num3++] = ' ';
				index3++;
			}
			return new string(chArray);
		}

		public static string EncodeHexString(byte[] buf)
		{
			if (buf != null)
			{
				return EncodeHexString(buf, 0, buf.Length);
			}
			else
			{
				return "";
			}
		}

		private static char HexDigit(int num)
		{
			return ((num < 10) ? ((char)(num + 0x30)) : ((char)(num + 0x37)));
		}

		protected static string EncodeHexStringFromInt(byte[] sArray)
		{
			string str = null;
			if (sArray == null)
			{
				return str;
			}
			char[] chArray = new char[sArray.Length * 2];
			int length = sArray.Length;
			int num3 = 0;
			while (length-- > 0)
			{
				int num = (sArray[length] & 240) >> 4;
				chArray[num3++] = HexDigit(num);
				num = sArray[length] & 15;
				chArray[num3++] = HexDigit(num);
			}
			return new string(chArray);
		}
		#endregion

		public static readonly char[] HexCharArray;

		private static char[] CreateHexChars()
		{
			char[] hexCharArray = new char[('9' - '0') + ('f' - 'a') + ('F' - 'A') + 3];
			int index = 0;
			for (int i = '0'; i <= '9'; i++)
			{
				hexCharArray[index++] = (char)i;
			}
			for (int i = 'a'; i <= 'f'; i++)
			{
				hexCharArray[index++] = (char)i;
			}
			for (int i = 'A'; i <= 'F'; i++)
			{
				hexCharArray[index++] = (char)i;
			}
			return hexCharArray;
		}

		static StringHexer()
		{
			HexCharArray = CreateHexChars();
		}
	}
}
