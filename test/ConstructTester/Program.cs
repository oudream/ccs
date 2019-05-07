using System;
using System.Collections.Generic;
using System.Text;

namespace ConstructTester
{
	class MyClassA
	{
		public MyClassA(string msg)
		{
			_msg = msg;
		}

		private string _msg;
		public string Msg
		{
			get
			{
				return _msg;
			}
			set
			{
				_msg = value;
			}
		}
	}

	class MyClassB : MyClassA
	{
		public MyClassB() : base("sdfaf")
		{
			Msg = "hello";
		}
	}


	class Program
	{
		static void Main(string[] args)
		{
			MyClassB cb = new MyClassB();


			Console.WriteLine(cb.Msg);

			Console.Read();
		}
	}
}
