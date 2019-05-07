using System;
using System.Collections.Generic;
using System.Text;

namespace CCS
{
 	public class CsThread
	{
        /// <summary>
        /// 线程休眠
        /// </summary>
        /// <param name="Milliseconds">毫秒数</param>
        /// <param name="ExitControlTag">强退出标记</param>
        public static void f_Sleep(System.Int32 Milliseconds, ref System.Boolean ExitControlTag)
        {
            System.DateTime _Origin = System.DateTime.Now;
            System.DateTime _Current = System.DateTime.Now;
            System.TimeSpan _TimeSpan = System.TimeSpan.Zero;
            while (!ExitControlTag)
            {
                _Current = System.DateTime.Now;
                _TimeSpan = _Current - _Origin;
                if (_TimeSpan.TotalMilliseconds >= Milliseconds) break;
                System.Threading.Thread.Sleep(1);
            }
        }

        /// <summary>
        /// 线程休眠
        /// </summary>
        /// <param name="Milliseconds">毫秒数</param>
        public static void f_Sleep(System.Int32 Milliseconds)
        {
            System.Threading.Thread.Sleep(Milliseconds);
        }
    }
}
