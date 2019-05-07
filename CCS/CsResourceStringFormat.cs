using System;
using System.Collections.Generic;
using System.Text;

namespace CCS
{
    /// <summary>
    /// 格式化单元
    /// </summary>
    public class CsResourceStringFormat
    {
        /// <summary>
        /// <para>日期时间(DateTime)的格式化字符串。24小时制，带前导零，精确到毫秒，横线分隔。</para>
        /// <para>0000-00-00 00:00:00.000</para>
        /// </summary>
        public const string DT_YMDHMSF_L = @"yyyy-MM-dd HH:mm:ss.fff";
        /// <summary>
        /// <para>日期时间(DateTime)的格式化字符串。24小时制，带前导零，精确到毫秒，斜线分隔。</para>
        /// <para>0000/00/00 00:00:00.000</para>
        /// </summary>
        public const string DT_YMDHMSF_S = @"yyyy/MM/dd HH:mm:ss.fff";
        /// <summary>
        /// <para>日期时间(DateTime)的格式化字符串。24小时制，带前导零，精确到秒，横线分隔。</para>
        /// <para>0000-00-00 00:00:00</para>
        /// </summary>
        public const string DT_YMDHMS_L = @"yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// <para>日期时间(DateTime)的格式化字符串。24小时制，带前导零，精确到秒，斜线分隔。</para>
        /// <para>0000/00/00 00:00:00</para>
        /// </summary>
        public const string DT_YMDHMS_S = @"yyyy/MM/dd HH:mm:ss";
        /// <summary>
        /// <para>日期时间(DateTime)的格式化字符串。日期部分，带前导零，横线分隔。</para>
        /// <para>0000-00-00</para>
        /// </summary>
        public const string DT_YMD_L = @"yyyy-MM-dd";
        /// <summary>
        /// <para>日期时间(DateTime)的格式化字符串。日期部分，带前导零，斜线分隔。</para>
        /// <para>0000/00/00</para>
        /// </summary>
        public const string DT_YMD_S = @"yyyy/MM/dd";
        /// <summary>
        /// <para>日期时间(DateTime)的格式化字符串。时间部分，24小时制，带前导零，精确到毫秒。</para>
        /// <para>00:00:00.000</para>
        /// </summary>
        public const string DT_HMSF = @"HH:mm:ss.fff";
        /// <summary>
        /// <para>日期时间(DateTime)的格式化字符串。时间部分，24小时制，带前导零，精确到秒。</para>
        /// <para>00:00:00</para>
        /// </summary>
        public const string DT_HMS = @"HH:mm:ss";
    }
}
