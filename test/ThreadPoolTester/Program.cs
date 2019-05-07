using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace ThreadPoolTester
{
    class Program
    {        
        static void Main(string[] args)
        {
			FileStream fs;
			byte[] buf = new byte[2];

			for (int i = 0; i < 20; i++)
            {
                ThreadPool.QueueUserWorkItem(
                    new WaitCallback(DoWork), i);

				fs = new FileStream(i + ".tmp",
				FileMode.Create, FileAccess.Write, FileShare.None,
				8192, false);
				buf[0] = (byte)i;
				fs.Write(buf, 0, buf.Length);
				fs.Close();
			}            
            Console.ReadLine();
        }

        static void DoWork(object state)
        {       
            int threadNumber = (int)state;

            Console.WriteLine("Thread {0} reporting for duty.", state);
             Console.WriteLine();
            
        }
    }
}
