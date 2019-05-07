using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace CSS
{
 	public class CsString
	{
		public static readonly char[] WhitespaceChars;

        public static readonly char[] HexCharArray;

        static CsString()
		{
			WhitespaceChars = new char[] { 
				'\t', '\n', '\v', '\f', '\r', ' ', '\x0085', '\x00a0', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', 
				' ', ' ', ' ', ' ', '​', '\u2028', '\u2029', '　', '﻿'
			};

            HexCharArray = CreateHexChars();
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
                if (!(IsValidHexChar(c) || IsWhitespaceChar(c)))
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
                if (IsValidHexChar(hexString[i]))
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












        /// <summary>
        /// 把字符串按照分隔符转换成 List
        /// </summary>
        /// <param name="str">源字符串</param>
        /// <param name="speater">分隔符</param>
        /// <param name="toLower">是否转换为小写</param>
        /// <returns></returns>
        public static List<string> GetStrArray(string str, char speater, bool toLower)
        {
            List<string> list = new List<string>();
            string[] ss = str.Split(speater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != speater.ToString())
                {
                    string strVal = s;
                    if (toLower)
                    {
                        strVal = s.ToLower();
                    }
                    list.Add(strVal);
                }
            }
            return list;
        }
        /// <summary>
        /// 把字符串转 按照, 分割 换为数据
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string[] GetStrArray(string str)
        {
            return str.Split(new Char[] { ',' });
        }
        /// <summary>
        /// 把 List<string> 按照分隔符组装成 string
        /// </summary>
        /// <param name="list"></param>
        /// <param name="speater"></param>
        /// <returns></returns>
        public static string GetArrayStr(List<string> list, string speater)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i]);
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(speater);
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 得到数组列表以逗号分隔的字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetArrayStr(List<int> list)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < list.Count; i++)
            {
                if (i == list.Count - 1)
                {
                    sb.Append(list[i].ToString());
                }
                else
                {
                    sb.Append(list[i]);
                    sb.Append(",");
                }
            }
            return sb.ToString();
        }
        /// <summary>
        /// 得到数组列表以逗号分隔的字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetArrayValueStr(Dictionary<int, int> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<int, int> kvp in list)
            {
                sb.Append(kvp.Value + ",");
            }
            if (list.Count > 0)
            {
                return DelLastComma(sb.ToString());
            }
            else
            {
                return "";
            }
        }


        #region 删除最后一个字符之后的字符

        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public static string DelLastComma(string str)
        {
            return str.Substring(0, str.LastIndexOf(","));
        }

        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public static string DelLastChar(string str, string strchar)
        {
            return str.Substring(0, str.LastIndexOf(strchar));
        }

        #endregion




        /// <summary>
        /// 转全角的函数(SBC case)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToSBC(string input)
        {
            //半角转全角：
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 32)
                {
                    c[i] = (char)12288;
                    continue;
                }
                if (c[i] < 127)
                    c[i] = (char)(c[i] + 65248);
            }
            return new string(c);
        }

        /// <summary>
        ///  转半角的函数(SBC case)
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        public static string ToDBC(string input)
        {
            char[] c = input.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                    continue;
                }
                if (c[i] > 65280 && c[i] < 65375)
                    c[i] = (char)(c[i] - 65248);
            }
            return new string(c);
        }

        /// <summary>
        /// 把字符串按照指定分隔符装成 List 去除重复
        /// </summary>
        /// <param name="o_str"></param>
        /// <param name="sepeater"></param>
        /// <returns></returns>
        public static List<string> GetSubStringList(string o_str, char sepeater)
        {
            List<string> list = new List<string>();
            string[] ss = o_str.Split(sepeater);
            foreach (string s in ss)
            {
                if (!string.IsNullOrEmpty(s) && s != sepeater.ToString())
                {
                    list.Add(s);
                }
            }
            return list;
        }


        #region 将字符串样式转换为纯字符串
        /// <summary>
        ///  将字符串样式转换为纯字符串
        /// </summary>
        /// <param name="StrList"></param>
        /// <param name="SplitString"></param>
        /// <returns></returns>
        public static string GetCleanStyle(string StrList, string SplitString)
        {
            string RetrunValue = "";
            //如果为空，返回空值
            if (StrList == null)
            {
                RetrunValue = "";
            }
            else
            {
                //返回去掉分隔符
                string NewString = "";
                NewString = StrList.Replace(SplitString, "");
                RetrunValue = NewString;
            }
            return RetrunValue;
        }
        #endregion

        #region 将字符串转换为新样式
        /// <summary>
        /// 将字符串转换为新样式
        /// </summary>
        /// <param name="StrList"></param>
        /// <param name="NewStyle"></param>
        /// <param name="SplitString"></param>
        /// <param name="Error"></param>
        /// <returns></returns>
        public static string GetNewStyle(string StrList, string NewStyle, string SplitString, out string Error)
        {
            string ReturnValue = "";
            //如果输入空值，返回空，并给出错误提示
            if (StrList == null)
            {
                ReturnValue = "";
                Error = "请输入需要划分格式的字符串";
            }
            else
            {
                //检查传入的字符串长度和样式是否匹配,如果不匹配，则说明使用错误。给出错误信息并返回空值
                int strListLength = StrList.Length;
                int NewStyleLength = GetCleanStyle(NewStyle, SplitString).Length;
                if (strListLength != NewStyleLength)
                {
                    ReturnValue = "";
                    Error = "样式格式的长度与输入的字符长度不符，请重新输入";
                }
                else
                {
                    //检查新样式中分隔符的位置
                    string Lengstr = "";
                    for (int i = 0; i < NewStyle.Length; i++)
                    {
                        if (NewStyle.Substring(i, 1) == SplitString)
                        {
                            Lengstr = Lengstr + "," + i;
                        }
                    }
                    if (Lengstr != "")
                    {
                        Lengstr = Lengstr.Substring(1);
                    }
                    //将分隔符放在新样式中的位置
                    string[] str = Lengstr.Split(',');
                    foreach (string bb in str)
                    {
                        StrList = StrList.Insert(int.Parse(bb), SplitString);
                    }
                    //给出最后的结果
                    ReturnValue = StrList;
                    //因为是正常的输出，没有错误
                    Error = "";
                }
            }
            return ReturnValue;
        }
        #endregion

        /// <summary>
        /// 分割字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="splitstr"></param>
        /// <returns></returns>
        public static string[] SplitMulti(string str, string splitstr)
        {
            string[] strArray = null;
            if ((str != null) && (str != ""))
            {
                strArray = new Regex(splitstr).Split(str);
            }
            return strArray;
        }
        public static string SqlSafeString(string String, bool IsDel)
        {
            if (IsDel)
            {
                String = String.Replace("'", "");
                String = String.Replace("\"", "");
                return String;
            }
            String = String.Replace("'", "&#39;");
            String = String.Replace("\"", "&#34;");
            return String;
        }

        #region 获取正确的Id，如果不是正整数，返回0
        /// <summary>
        /// 获取正确的Id，如果不是正整数，返回0
        /// </summary>
        /// <param name="_value"></param>
        /// <returns>返回正确的整数ID，失败返回0</returns>
        public static int StrToId(string _value)
        {
            if (IsNumberId(_value))
                return int.Parse(_value);
            else
                return 0;
        }
        #endregion
        #region 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。
        /// <summary>
        /// 检查一个字符串是否是纯数字构成的，一般用于查询字符串参数的有效性验证。(0除外)
        /// </summary>
        /// <param name="_value">需验证的字符串。。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool IsNumberId(string _value)
        {
            return QuickValidate("^[1-9]*[0-9]*$", _value);
        }
        #endregion
        #region 快速验证一个字符串是否符合指定的正则表达式。
        /// <summary>
        /// 快速验证一个字符串是否符合指定的正则表达式。
        /// </summary>
        /// <param name="_express">正则表达式的内容。</param>
        /// <param name="_value">需验证的字符串。</param>
        /// <returns>是否合法的bool值。</returns>
        public static bool QuickValidate(string _express, string _value)
        {
            if (_value == null) return false;
            Regex myRegex = new Regex(_express);
            if (_value.Length == 0)
            {
                return false;
            }
            return myRegex.IsMatch(_value);
        }
        #endregion


        #region 根据配置对指定字符串进行 MD5 加密
        /// <summary>
        /// 根据配置对指定字符串进行 MD5 加密
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GetMD5(string sInput)
        {
            byte[] result = Encoding.Default.GetBytes(sInput);  
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "");  //tbMd5pass为输出加密文本的文本框  
            //md5加密
            //s = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(s, "md5").ToString();
            //return r.ToLower().Substring(8, 16);
        }
        #endregion

        #region 得到字符串长度，一个汉字长度为2
        /// <summary>
        /// 得到字符串长度，一个汉字长度为2
        /// </summary>
        /// <param name="inputString">参数字符串</param>
        /// <returns></returns>
        public static int StrLength(string inputString)
        {
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;
            }
            return tempLen;
        }
        #endregion

        #region 截取指定长度字符串
        /// <summary>
        /// 截取指定长度字符串
        /// </summary>
        /// <param name="inputString">要处理的字符串</param>
        /// <param name="len">指定长度</param>
        /// <returns>返回处理后的字符串</returns>
        public static string ClipString(string inputString, int len)
        {
            bool isShowFix = false;
            if (len % 2 == 1)
            {
                isShowFix = true;
                len--;
            }
            System.Text.ASCIIEncoding ascii = new System.Text.ASCIIEncoding();
            int tempLen = 0;
            string tempString = "";
            byte[] s = ascii.GetBytes(inputString);
            for (int i = 0; i < s.Length; i++)
            {
                if ((int)s[i] == 63)
                    tempLen += 2;
                else
                    tempLen += 1;

                try
                {
                    tempString += inputString.Substring(i, 1);
                }
                catch
                {
                    break;
                }

                if (tempLen > len)
                    break;
            }

            byte[] mybyte = System.Text.Encoding.Default.GetBytes(inputString);
            if (isShowFix && mybyte.Length > len)
                tempString += "…";
            return tempString;
        }
        #endregion



        #region HTML转行成TEXT
        /// <summary>
        /// HTML转行成TEXT
        /// </summary>
        /// <param name="strHtml"></param>
        /// <returns></returns>
        public static string HtmlToTxt(string strHtml)
        {
            string[] aryReg ={
            @"<script[^>]*?>.*?</script>",
            @"<(\/\s*)?!?((\w+:)?\w+)(\w+(\s*=?\s*(([""'])(\\[""'tbnr]|[^\7])*?\7|\w+)|.{0})|\s)*?(\/\s*)?>",
            @"([\r\n])[\s]+",
            @"&(quot|#34);",
            @"&(amp|#38);",
            @"&(lt|#60);",
            @"&(gt|#62);",
            @"&(nbsp|#160);",
            @"&(iexcl|#161);",
            @"&(cent|#162);",
            @"&(pound|#163);",
            @"&(copy|#169);",
            @"&#(\d+);",
            @"-->",
            @"<!--.*\n"
            };

            string newReg = aryReg[0];
            string strOutput = strHtml;
            for (int i = 0; i < aryReg.Length; i++)
            {
                Regex regex = new Regex(aryReg[i], RegexOptions.IgnoreCase);
                strOutput = regex.Replace(strOutput, string.Empty);
            }

            strOutput.Replace("<", "");
            strOutput.Replace(">", "");
            strOutput.Replace("\r\n", "");


            return strOutput;
        }
        #endregion

        #region 判断对象是否为空
        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <typeparam name="T">要验证的对象的类型</typeparam>
        /// <param name="data">要验证的对象</param>        
        public static bool IsNullOrEmpty<T>(T data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }

        /// <summary>
        /// 判断对象是否为空，为空返回true
        /// </summary>
        /// <param name="data">要验证的对象</param>
        public static bool IsNullOrEmpty(object data)
        {
            //如果为null
            if (data == null)
            {
                return true;
            }

            //如果为""
            if (data.GetType() == typeof(String))
            {
                if (string.IsNullOrEmpty(data.ToString().Trim()))
                {
                    return true;
                }
            }

            //如果为DBNull
            if (data.GetType() == typeof(DBNull))
            {
                return true;
            }

            //不为空
            return false;
        }
        #endregion
    }
}
