using System;
using System.Diagnostics;
using System.ComponentModel;

public class MatchStarter
{
    public static void StartMatch()
    {
        //Process myProcess = new Process();

        ProcessStartInfo startInfo = new ProcessStartInfo();
        startInfo.CreateNoWindow = false;
        startInfo.UseShellExecute = false;
        startInfo.FileName = "../../../../Test2/GameTest.exe";
        startInfo.WindowStyle = ProcessWindowStyle.Hidden;

        try
        {
            //startInfo.Arguments = "-f j -o \"" + ex1 + "\" -z 1.0 -s y " + ex2;

            //myProcess.StartInfo.UseShellExecute = false;

            //myProcess.StartInfo.FileName = "GameTest.exe";
            //myProcess.StartInfo.CreateNoWindow = false;
            using (Process process0 = Process.Start(startInfo))
            {
                using (Process process1 = Process.Start(startInfo))
                {
                    process1.WaitForExit();
                }
                process0.WaitForExit();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}