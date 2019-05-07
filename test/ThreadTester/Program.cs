using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ThreadTester
{
	class Program
	{

		public static ManualResetEvent _stopEvent = null;

		public static void ReceiveServerData()
		{
			Console.WriteLine("ReceiveServerData begin");

			//如果未接收到停止信号,则继续循环接受
			while (! _stopEvent.WaitOne(0))
			{
				Console.WriteLine("WaitOne out time");
				//_stopEvent.Reset();
				//Thread.Sleep(2000);
			}

			Console.WriteLine("ReceiveServerData end");
		}

		static void Main(string[] args)
		{
			Thread _recThread;

			_stopEvent = new ManualResetEvent(false);
			
			_recThread = new Thread(new ThreadStart(ReceiveServerData));
			_recThread.IsBackground = true;
			_recThread.Start();

			ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
			while (keyInfo.Key != ConsoleKey.Escape)
			{
				keyInfo = Console.ReadKey();

				if (keyInfo.Key == ConsoleKey.Enter)
				{
					_stopEvent.Set();
				}
				else if (keyInfo.Key== ConsoleKey.B)
				{
					if (! _recThread.Join(3000))
					{
						_recThread.Abort();
					}
					_recThread = null;
				}
				else
				{
					_stopEvent.Reset();
					Console.WriteLine("Please Press Key ESC");
				}
			}
		}
	}
}
