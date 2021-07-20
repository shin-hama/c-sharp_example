using System;
using System.Text.RegularExpressions;


namespace utility
{
    public class RegexUtil
    {
        public static void PerseEscape()
        {
            // backslash is escaped in regex pattern, so use `\\\\`
            Regex escapePattern = new Regex("[\\\\\"\n\r\t]");
            string test = "\" test \\ backslash \t tab\"";
            Console.WriteLine(test);

            var value = escapePattern.Replace(test, "\\$0");

            Console.WriteLine(value);
        }
    }
}
