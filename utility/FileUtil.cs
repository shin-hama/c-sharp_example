using System;
using System.IO;

namespace utility
{
    public class FileUtil
    {
        /// <summary>
        /// ファイル名がなければ作成不可、ただし拡張子だけなら作成される
        /// </summary>
        public static void CreateEmptyName()
        {
            File.Create(".txt");  // True
            // File.Create(" ");  // False
            // File.Create(".");  // false
            // File.Create("");  // System.ArgumentException
        }

        /// <summary>
        /// サブディレクトリを含めて作成
        /// </summary>
        public static void CreateSubDirectory()
        {
            Directory.CreateDirectory("./test/test/test");  // True
        }

        public static void DirectoryInfoUtil()
        {
            var di = new DirectoryInfo("./test1/test2");
            Console.WriteLine(di.Name);  // test2

            di.Create();  // Create all subdirectory
        }
    }
}
