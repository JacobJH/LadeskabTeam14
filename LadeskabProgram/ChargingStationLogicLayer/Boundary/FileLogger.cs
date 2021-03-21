using System;
using System.IO;
using LogicLayer.Boundary.Interfaces;

namespace LogicLayer.Boundary
{
    public class FileLogger : ILogger
    {
        private const string filePath = @"";

        public void LogDoorLocked(int id)
        {
            StreamWriter writer = File.AppendText(filePath + "LogFile.txt");
            writer.WriteLine("Door looked by " + id);
            writer.Flush();
            writer.Close();
        }

        public void LogDoorUnLocked(int id)
        {
            StreamWriter writer = File.AppendText(filePath + "LogFile.txt");
            writer.WriteLine("Door unlocked by " + id);
            writer.Flush();
            writer.Close();
        }
    }
}