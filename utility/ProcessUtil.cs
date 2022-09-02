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
                startInfo.Arguments = "/c start C:\\PROGRA~1\\JEOL\\AutomationCenter\\bin\\Release\\AutomationCenter.exe C:\\Users\\Public\\Documents\\JEOL\\AutomationCenter\\Recipe\\MultiSpecPorter\\Temp\\9-recipe.jac";
                startInfo.Verb = "RunAs";
                startInfo.WorkingDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                //startInfo.WorkingDirectory = "";                     //先にAnacondaを入れるため、WorkingDirectoryはどこでもよくなった

                var processInfo = new ProcessStartInfo
                {
                    FileName = "cmd.exe",
                    CreateNoWindow = false,
                    UseShellExecute = true,
                    Arguments = "/c start C:\\PROGRA~1\\JEOL\\AutomationCenter\\bin\\Release\\AutomationCenter.exe C:\\Users\\Public\\Documents\\JEOL\\AutomationCenter\\Recipe\\MultiSpecPorter\\Temp\\9-recipe.jac & pause"
                };

                Process process = new Process();
                process.StartInfo = startInfo;
                process.Start();
                process.WaitForExit();

                var versionStr = process.StandardOutput.ReadToEnd().Trim();
                Console.WriteLine(versionStr);
                Version.TryParse(versionStr, out var version);
                Console.WriteLine(version);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
