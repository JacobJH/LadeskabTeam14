using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using EventArguments;
using LogicLayer.Boundary;
using NUnit.Framework;

namespace ChargingStationProgram.UnitTest.Boundarytests
{
    [TestFixture]
    public class TestConsoleDisplay
    {
        private ConsoleDisplay uut;

        private StringWriter output;

        [SetUp]
        public void Setup()
        {
            //arrange 
            uut = new ConsoleDisplay(); //Door closed and unlocked by default

            output = new StringWriter();

            Console.SetOut(output);

        }

        [TestCase("Hej")]
        [TestCase("")]
        [TestCase("Hallo")]
        [TestCase("Ladeskab er åbnet")]
        [TestCase(null)]
        public void DisplayMessage_DifferentMessages_AllMessagesWrittenToConsoleCorrectly(string s)
        {
            //act
            uut.DisplayMessage(s);

            //assert
            Assert.That(output.ToString(),Is.EqualTo(s+"\r\n"));
        }


    }
}
