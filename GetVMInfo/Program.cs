using System;
using System.Diagnostics;
using System.IO;

namespace GetVMInfo
{
    class Program
    {
        static void Main()
        {
            LaunchCommandLineApp();
        }

        /// <summary>
        /// Launch the legacy application with some options set.
        /// </summary>
        static void LaunchCommandLineApp()
        {
            // For the example.
            // C:\Windows\System32\cmd.exe /c ""C:\Program Files\Oracle\VirtualBox\VBoxManage.exe" guestproperty enumerate "Centos 7 - Server Apache" & pause"

            // Use ProcessStartInfo class.
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = false;
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.FileName = @"C:\Windows\System32\cmd.exe";
            //startInfo.WindowStyle = ProcessWindowStyle.Normal;
            //startInfo.Arguments = @"/c 'C:\Program Files\Oracle\VirtualBox\VBoxManage.exe' guestproperty enumerate 'Centos 7 - Server Apache'".Replace('\'', '\"');
            //startInfo.Arguments = @"/c ""C:\\Program Files\\Oracle\\VirtualBox\\VBoxManage.exe"" guestproperty enumerate ""Centos 7 - Server Apache""";
            startInfo.Arguments = String.Format(@"/c {0}C:/Program Files/Oracle/VirtualBox/VBoxManage.exe{0} guestproperty enumerate Centos62VM", '"');

            try
            {
                Console.WriteLine("Arg : >" + startInfo.Arguments + "<");
                // Start the process with the info we specified.
                // Call WaitForExit and then the using-statement will close.
                using (Process exeProcess = Process.Start(startInfo))
                {
                    Console.WriteLine("---INFO---");
                    using (StreamReader reader = exeProcess.StandardOutput)
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine(result);
                    }
                    Console.WriteLine("---ERROR---");
                    using (StreamReader reader = exeProcess.StandardError)
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine(result);
                    }
                }
            }
            catch (Exception exc)
            {
                // Log error.
                Console.WriteLine(exc.Message);
            }
        }
    }
}