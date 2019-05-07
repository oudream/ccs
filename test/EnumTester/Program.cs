using System;
using System.Collections.Generic;
using System.Text;

namespace EnumTester
{
	class Program
	{
		enum MyEnum
		{
			a,
			b,
			c,
			d
		}

		abstract class MyClassA
		{
			
		}

		class MyClass<T, D> : MyClassA
		{
			public MyClass(T obj)
			{
				_myProperty = obj;
			}

			private T _myProperty;
			public T MyProperty
			{
				get
				{
					return _myProperty;
				}
				set
				{
					_myProperty = value;
				}
			}
		}

        static void asdfafd(int[] a)
        { 
        }

		static void Main(string[] args)
		{
            asdfafd(new int[]{1, 2});
            string[] ss = { "1", "2" };

			MyEnum e = MyEnum.d;

			string es = "c";

			object o = e;

			if (o is MyEnum)
			{
				o = Enum.Parse(o.GetType(), es);
			}

			Console.WriteLine(o.ToString());


			MyClass<MyEnum, object> mc = new MyClass<MyEnum, object>(MyEnum.b);

			MyClass<MyEnum, object> mo = mc;

			Console.WriteLine(mc.MyProperty.ToString());

			Console.Read();
		}
	}
}
