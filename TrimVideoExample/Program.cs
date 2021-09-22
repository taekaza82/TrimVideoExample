using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace TrimVideoExample
{
    class Program
    {
        static void Main(string[] args)
        {
            string videoFile = @"D:\VDO\vcr0051920210920091418.mp4";
            string outputFile = @"D:\VDO\vcr0051920210920091418-trimed.mp4";

            TrimVideo(videoFile, outputFile, "00:00:00", "00:00:30");

            Console.ReadLine();
        }

        static void TrimVideo(string videoFile, string outputFile, string start, string end)
        {
            Console.WriteLine("Start Triming Video...");
        

            string ffmpegFile = AppDomain.CurrentDomain.BaseDirectory + @"ffmpeg.exe";
            string command = @"-ss {0} -i ""{1}"" -to {2} -c copy ""{3}""";
            Process process = new Process();
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.FileName = ffmpegFile; 
            process.StartInfo.Arguments = string.Format(command, start, videoFile, end, outputFile);
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.CreateNoWindow = false;
            if(!process.Start())
            {
                Console.WriteLine("Error starting");
                return;
            }
            StreamReader reader = process.StandardError;
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
            process.Close();

            Console.WriteLine("Finished Triming Video");
        }
    }
}
