using System.Reflection;
using System;
public class Program
{

    class ClassA<T>
    {
        private T _value;
        public T Value
        {
            get
            {
                return _value;
            }
        }
    }

    class ClassB : ClassA<int>
    {
        public void adfadf<K>()
        {
            K a;
        }
    }

	public static void Main()
	{
        Type t = typeof(ClassB).BaseType;
        
        if (t.IsGenericType)
        {
            // If this is a generic type, display the type arguments.
            //
            Type[] typeArguments = t.GetGenericArguments();

            Console.WriteLine("\tList type arguments ({0}):",
                typeArguments.Length);

            foreach (Type tParam in typeArguments)
            {
                // If this is a type parameter, display its
                // position.
                //
                if (tParam.IsGenericParameter)
                {
                    Console.WriteLine("\t\t{0}\t(unassigned - parameter position {1})",
                        tParam,
                        tParam.GenericParameterPosition);
                }
                else
                {
                    Console.WriteLine("\t\t{0}", tParam);
                }
            }
        }

        Console.ReadLine();
	}
}



