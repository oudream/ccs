using System;
using System.Collections.Generic;
using System.Text;

namespace GenericTester
{
    public class Class1<T>
    {
        private T _value;
        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }
    }
}
