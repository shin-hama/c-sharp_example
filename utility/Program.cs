using System;
using System.Diagnostics;

namespace utility
{
    class Program
    {
        static void Main(string[] args)
        {
            new FileMonitor().Main();
            // ProcessUtil.RunProcess();
        }

        static void TestProcess()
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                CreateNoWindow = false,
                UseShellExecute = false,
                Arguments = "/c activate VirtualAC > nul 2>&1 & ipython -V 2>&1 "
            };

            using (var process = Process.Start(processInfo))
            {
                try
                {
                    var versionStr = process.StandardOutput.ReadToEnd().Trim();
                    Console.WriteLine(versionStr);
                    process.WaitForExit();
                    if (!Version.TryParse(versionStr, out var version))
                    {
                        throw new Exception($"Failed to parse the installed IPython version : [ {versionStr} ]");
                    }
                    Console.WriteLine(version);

                    if (version.Major < 4)
                    {
                        throw new Exception("Installed IPython version is too old.");
                    }
                }
                finally
                {
                    if (!process.HasExited)
                    {
                        try
                        {
                            process.Kill();
                        }
                        catch
                        {
                            // do nothing
                        }
                    }
                }
            }
        }
    }
}
