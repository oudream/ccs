using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceTester
{
	interface IClassA
	{
		string GetName();
	}

	class ClassA : IClassA
	{

		#region IClassA 成员

		public int MyProperty { get; set; }

		string IClassA.GetName()
		{
			return "HelloA";
		}

		#endregion
	}

	class ClassB
	{
		string GetName()
		{
			return "HelloB";
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			ClassB b = new ClassB();
			IClassA ia = b as IClassA;
			if (ia != null)
			{
				Console.WriteLine(ia.GetName());
			}
			Console.Read();
		}
	}
}
