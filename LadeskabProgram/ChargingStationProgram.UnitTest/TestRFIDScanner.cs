using System;
using System.Collections.Generic;
using System.Text;
using EventArguments;
using LogicLayer.Boundary;
using NUnit.Framework;

namespace ChargingStationProgram.UnitTest
{
    [TestFixture]
    public class TestRFIDScanner
    {
        private RFIDReader uut;
        private RFIDDetectedArgs RFIDArgs;


        [SetUp]
        public void Setup()
        {
            //arrange 
            uut = new RFIDReader();

            RFIDArgs = null;

            uut.RFIDReaderEvent += (o, args) =>
            {
                RFIDArgs = args;
            };
        }

        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        public void TestEventIsNotNullAfterRFIDRead(int ID)
        {
            //act 
            uut.OnRfidRead(ID);

            //assert
            Assert.That(RFIDArgs, Is.Not.Null);
        }


        [TestCase(1)]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        [TestCase(int.MinValue)]
        [TestCase(int.MaxValue)]
        public void TestEventIsCorrectValueAfterRFIDRead(int ID)
        {
            //act 
            uut.OnRfidRead(ID);

            //assert
            Assert.That(RFIDArgs.IncomingRFIDFromScanner, Is.EqualTo(ID));
        }

    }
}
