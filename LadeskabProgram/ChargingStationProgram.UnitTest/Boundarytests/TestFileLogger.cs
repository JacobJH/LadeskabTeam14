using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LogicLayer.Boundary;

namespace ChargingStationProgram.UnitTest
{
    [TestFixture]
    public class TestFileLogger
    {
        private FileLogger _uut;
        private const string filePath = @"";


        [SetUp]
        public void Setup()
        {
            _uut = new FileLogger();
        }

        [TestCase(1)]
        [TestCase(20)]
        public void LogDoorIsLocked(int id)
        {
            //arrange 

            //act 
            _uut.LogDoorLocked(id);

            //assert
            string[] loadFile = File.ReadAllLines(filePath + "LogFile.txt");
            Assert.That(loadFile[loadFile.Length-1],Is.EqualTo("Door looked by " + id));
            
        }

        [TestCase(1)]
        [TestCase(20)]
        public void LogDoorIsUnlocked(int id)
        {
            //arrange 

            //act 
            _uut.LogDoorUnLocked(id);

            //assert
            string[] loadFile = File.ReadAllLines(filePath + "LogFile.txt");
            Assert.That(loadFile[loadFile.Length - 1], Is.EqualTo("Door unlocked by " + id));

        }
    }
}
