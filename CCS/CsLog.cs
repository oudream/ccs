using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace CCS
{
    /// <summary>
    /// 日志。
    /// <para>***********************************************************</para>
    /// <para>使用步骤：</para>
    /// <para>1.设置Enabled = true：启用日志功能。</para>
    /// <para>3.设置Enabled = false：停用日志功能。</para>
    /// <para>***********************************************************</para>
    /// </summary>
    public class CsLog
    {
        #region 静态构造
        /// <summary>
        /// 静态构造，仅用于静态属性的初始化，由.Net自动调用。
        /// </summary>
        static CsLog()
        {
            _v_queue = new Queue<xLogUnit>();
            _v_cts = new CancellationTokenSource();
            _v_thread_is_exit = true;
        }
        #endregion
        #region 私有属性
        /// <summary>
        /// 内部线程对象
        /// </summary>
        private static Thread _v_thread = null;
        /// <summary>
        /// 日志队列
        /// </summary>
        private static Queue<xLogUnit> _v_queue = null;
        /// <summary>
        /// 内部线程停止事件
        /// </summary>
        private static CancellationTokenSource _v_cts = null;
        /// <summary>
        /// 内部线程是否退出标记。
        /// <para>True：已退出； False：未退出</para>
        /// </summary>
        private static bool _v_thread_is_exit;
        /// <summary>
        /// 标准版日志格式化打印字符串。
        /// <para>参1：产生时间</para>
        /// <para>参2：发生接口（函数名）</para>
        /// <para>参3：日志等级</para>
        /// <para>参4：日志文本</para>
        /// <para>参5：换行符</para>
        /// </summary>
        private const string _v_log_format = "[{0}] @{1}() {2}>> {3}{4}";
        /// <summary>
        /// 简版日志格式化打印字符串。
        /// <para>参1：产生时间</para>
        /// <para>参2：日志文本</para>
        /// </summary>
        private const string _v_log_format_simple = "[{0}] >> {1}";
        #endregion
        #region 私有方法
        /// <summary>
        /// 内部线程入口
        /// </summary>
        private static void _v_thread_enter()
        {
            _v_thread_is_exit = false;
            while (true)
            {
                try
                {
                    if (_v_cts.Token.IsCancellationRequested) break;
                    if (_v_queue != null)
                    {
                        if (_v_queue.Count > 0)
                        {
                            xLogUnit unit = _v_queue.Dequeue();
                            if (unit == null) continue;
                            if (unit._Level > LevelSwitch) continue;
                            FileStream fs = new FileStream(GetLogFile(unit), FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
                            sw.Write(_v_get_string(unit));
                            sw.Close();
                            fs.Close();
                        }
                    }
                }
                catch (System.Exception ex)
                {

                }
                CsThread.f_Sleep(5);
            }
            _v_thread_is_exit = true;
        }

        private static string _v_get_string(xLogUnit _unit)
        {
            StringBuilder sbResult = new StringBuilder(null);
            if (_unit._Type == eLogType.SIMPLE)
            {
                sbResult.AppendFormat(_v_log_format_simple,
                    _unit._Time.ToString(CsResourceStringFormat.DT_YMDHMSF_L),
                    _unit._Text + Environment.NewLine);
            }
            else
            {
                sbResult.AppendFormat(_v_log_format,
                    _unit._Time.ToString(CsResourceStringFormat.DT_YMDHMSF_L),
                    _unit._Function,
                    _unit._Level.ToString(),
                    _unit._Text,
                    Environment.NewLine);
            }
            return sbResult.ToString();
        }
        #endregion
        #region 公共属性
        /// <summary>
        /// <para>日志等级开关。</para>
        /// <para>只存储设定等级和该等级以上的日志信息。</para>
        /// </summary>
        public static eLogLevel LevelSwitch = eLogLevel.L5;
        /// <summary>
        /// 启用日志功能。
        /// </summary>
        public static bool Enabled
        {
            get { return (_v_thread == null) ? false : true; }
            set
            {
                if (value)
                {
                    if (_v_thread == null)
                    {
                        _v_thread = new Thread(new ThreadStart(_v_thread_enter));
                        _v_thread.IsBackground = true;
                        _v_thread.Start();
                    }
                }
                else
                {
                    _v_queue.Clear();
                    _v_cts.Cancel();
                    while (!_v_thread_is_exit) CsThread.f_Sleep(5);
                    _v_thread = null;
                }
            }
        }
        #endregion
        #region 公共方法
        /// <summary>
        /// 根据日志类型提取日志文件
        /// </summary>
        /// <param name="_type">日志类型</param>
        /// <returns></returns>
        private static string GetLogFile(xLogUnit _unit)
        {
            DateTime _Now = DateTime.Now;
            StringBuilder sbLogFile = new StringBuilder(null);
            sbLogFile.AppendFormat("{0}{1}\\", System.AppDomain.CurrentDomain.BaseDirectory, _unit._Time.ToString(CsResourceStringFormat.DT_YMD_L) + @"\");
            if (!Directory.Exists(sbLogFile.ToString())) Directory.CreateDirectory(sbLogFile.ToString());
            if (_unit._Type == eLogType.SIMPLE)
            {
                sbLogFile.AppendFormat("{0}.Log.{1}.log",
                    _unit._Time.Hour.ToString("D2"),
                    Path.GetFileNameWithoutExtension(AppDomain.CurrentDomain.FriendlyName));
            }
            else
            {
                sbLogFile.AppendFormat("{0}.Log[{1}].log",
                    _unit._Time.Hour.ToString("D2"),
                    _unit._Type.ToString().ToLower());
            }
            if (!File.Exists(sbLogFile.ToString())) using (File.Create(sbLogFile.ToString())) { }
            return sbLogFile.ToString();
        }
        public static string Add(string _text)
        {
            xLogUnit unit = new xLogUnit();
            unit._Text = _text;
            unit._Type = eLogType.SIMPLE;
            unit._Function = null;
            unit._Time = DateTime.Now;
            unit._Level = eLogLevel.L5;
            return Add(unit);
        }
        /// <summary>
        /// 添加日志。只需提供：日志单元。
        /// </summary>
        /// <param name="_unit">日志单元</param>
        /// <returns>标准化日志信息</returns>
        private static string Add(xLogUnit _unit)
        {
            if (_v_queue != null && Enabled) _v_queue.Enqueue(_unit);
            return _v_get_string(_unit);
        }
        /// <summary>
        /// 添加日志。需提供：日志文本、日志类型、产生接口（函数名）、日志等级。
        /// </summary>
        /// <param name="_text">日志文本</param>
        /// <param name="_type">日志类型</param>
        /// <param name="_function">产生接口（函数名）</param>
        /// <param name="_level">日志等级</param>
        /// <returns>标准化日志信息</returns>
        private static string Add(string _text, eLogType _type, string _function, eLogLevel _level)
        {
            xLogUnit unit = new xLogUnit();
            unit._Text = _text;
            unit._Type = _type;
            unit._Function = _function;
            unit._Time = DateTime.Now;
            unit._Level = _level;
            return Add(unit);
        }
        /// <summary>
        /// 添加日志。需提供：日志文本、日志类型、产生接口（函数名）、日志等级、产生时间。
        /// </summary>
        /// <param name="_text">日志文本</param>
        /// <param name="_type">日志类型</param>
        /// <param name="_function">产生接口（函数名）</param>
        /// <param name="_level">日志等级</param>
        /// <param name="_time">产生时间</param>
        /// <returns>标准化日志信息</returns>
        private static string Add(string _text, eLogType _type, string _function, eLogLevel _level, DateTime _time)
        {
            xLogUnit unit = new xLogUnit();
            unit._Text = _text;
            unit._Type = _type;
            unit._Function = _function;
            unit._Time = _time;
            unit._Level = _level;
            return Add(unit);
        }
        /// <summary>
        /// 清除日志缓冲区
        /// </summary>
        private static void Clear()
        {
            _v_queue.Clear();
        }
        #endregion
    }

    /// <summary>
    /// 日志体
    /// </summary>
    public class xLogUnit
    {
        /// <summary>
        /// 日志内容
        /// </summary>
        public string _Text;
        /// <summary>
        /// 所属接口
        /// </summary>
        public string _Function;
        /// <summary>
        /// 产生时间
        /// </summary>
        public DateTime _Time;
        /// <summary>
        /// 日志类型
        /// </summary>
        public eLogType _Type;
        /// <summary>
        /// 日志等级
        /// </summary>
        public eLogLevel _Level;
    }

    /// <summary>
    /// 日志类型
    /// </summary>
    public enum eLogType
    {
        /// <summary>
        /// 简版日志
        /// </summary>
        SIMPLE = 0,
        /// <summary>
        /// 系统日志
        /// </summary>
        SYSTEM = 1,
        /// <summary>
        /// 程序日志
        /// </summary>
        PROGRAM = 2,
        /// <summary>
        /// 线程日志
        /// </summary>
        THREAD = 3,
        /// <summary>
        /// 错误日志
        /// </summary>
        ERROR = 4,
        /// <summary>
        /// 异常日志
        /// </summary>
        EXCEPTION = 5,
        /// <summary>
        /// 数据日志
        /// </summary>
        DATA = 6
    }

    /// <summary>
    /// 日志等级
    /// </summary>
    public enum eLogLevel
    {
        /// <summary>
        /// 一级日志，致命型信息。
        /// </summary>
        L1 = 1,
        /// <summary>
        /// 二级日志，重要型信息。
        /// </summary>
        L2 = 2,
        /// <summary>
        /// 三级日志，一般型信息。
        /// </summary>
        L3 = 3,
        /// <summary>
        /// 四级日志，轻量型信息。
        /// </summary>
        L4 = 4,
        /// <summary>
        /// 五级日志，提示型信息。
        /// </summary>
        L5 = 5
    }
}
