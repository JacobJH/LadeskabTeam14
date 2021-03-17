using System;
using System.IO;
using LogicLayer.Boundary.Interfaces;

namespace LogicLayer.Boundary
{
    public class FileLogger : ILogger
    {
        private const string filePath = @"";

        public FileLogger()
        {
            File.Create(filePath + "LogFile.txt");
        }


        public void LogDoorLocked(int id)
        {
            File.AppendText("Door looked by " + id);
        }

        public void LogDoorUnLocked(int id)
        {
            File.AppendText("Door opened by " + id);
        }
    }
}