using System;
using System.Diagnostics;


namespace utility
{
    public class ProcessUtil
    {
        public static void RunProcess()
        {
            try
            {
                //string PyJEMPath = this.Context.Parameters["dir"];

                string arg = string.Join(" ", new[] { "/c", "python", @"D:\workspace\test\python\main.py", "--test", "test" });

                var startInfo = new ProcessStartInfo();
                startInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec");
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.Arguments = arg;
                startInfo.Verb = "RunAs";
                startInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //startInfo.WorkingDirectory = "";                     //先にAnacondaを入れるため、WorkingDirectoryはどこでもよくなった

                Process process = Process.Start(startInfo);
                process.WaitForExit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
