using System;
using System.Collections.Generic;

namespace utility
{
    public class StringUtil
    {
        public static void StringJoin()
        {
            Console.WriteLine(string.Join("_", new[] { "1", "2", "2", "s" }));
        }

        public static void ObjectToString()
        {
            object a = null;
            Console.WriteLine(a.ToString());  // Raise NullReference
        }

        public static void VerifyString()
        {
            string a = null;
            Console.WriteLine(a is string);  // False

            object b = "test";
            Console.WriteLine(b is string);  // True
            Console.WriteLine(b.GetType() == typeof(string));  // True
            // Console.WriteLine(b.GetType() is typeof(string));  // Error
        }
    }
}
