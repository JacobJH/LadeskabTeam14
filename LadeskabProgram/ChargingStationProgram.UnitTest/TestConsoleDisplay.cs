using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LogicLayer.Boundary;

namespace UsbSimulator.Test
{
    [TestFixture]
    public class TestConsoleDisplay
    {
        private ConsoleDisplay _uut;
        [SetUp]
        public void Setup()
        {
            _uut = new ConsoleDisplay();
        }

        [Test]
        public void DisplayMessage_isCalled()
        {
            _uut.DisplayMessage("test");

        }
    }
}
