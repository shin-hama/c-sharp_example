using System;
using System.Collections.Generic;

namespace utility
{
    public class EnumUtil
    {
        enum test
        {
            hoge,
            bar,
            foo,
            fuga,
            none,
            test,
        }
        public static void FindValue()
        {
            List<test> l = new List<test> { test.bar, test.foo };

            Console.WriteLine($"foo in foo, bar = {l.Contains(test.foo)}");
            Console.WriteLine($"fuga in foo, bar = {l.Contains(test.fuga)}");
        }

    }
}
