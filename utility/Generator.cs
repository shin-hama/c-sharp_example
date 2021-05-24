using System;
using System.Collections.Generic;

namespace utility
{
    public class Generator
    {
        public static void GeneratorUtil()
        {
            foreach (var item in YieldReturnTest())
            {
                Console.WriteLine(item);
            }
        }

        private static IEnumerable<string> YieldReturnTest()
        {
            bool isTest = true;
            if (isTest)
            {
                yield break;
            }
            var test = new List<string>() { "hoge", "fuga", "bar" };
            foreach (string item in test)
            {
                yield return item;
            }
        }

    }
}
