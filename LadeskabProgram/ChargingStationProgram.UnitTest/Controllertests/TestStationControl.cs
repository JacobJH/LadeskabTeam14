using System;
using System.Collections.Generic;
using System.Text;
using EventArguments;
using LogicLayer.Boundary;
using LogicLayer.Boundary.Interfaces;
using LogicLayer.Controllers;
using NSubstitute;
using NUnit.Framework;

namespace ChargingStationProgram.UnitTest
{
    [TestFixture]
    public class TestStationControl
    {
        private StationControl uut;
        private IDoor door;
        private IRFID rfid;
        private IChargeControl charger;
        private IDisplay disp;
        private ILogger logger;


        [SetUp]
        public void setup()
        {
            //Opsætter unit under test og tilsvarende boundaries
            door = Substitute.For<IDoor>();
            rfid = Substitute.For<IRFID>();
            charger = Substitute.For<IChargeControl>();
            disp = Substitute.For<IDisplay>();
            logger = Substitute.For<ILogger>();

            uut = new StationControl(charger, door, disp, logger, rfid);
        }


        //InteractionTest
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(10)]
        [TestCase(100)]
        public void RfidDetected_StateIsClosed_shouldChangeStateAndCallLockDoorOnDoor(int id)
        {
            charger.Connected = true;
            
            rfid.RFIDReaderEvent += Raise.EventWith(new RFIDDetectedArgs{});



            Assert.Multiple(() =>
                {
                    door.Received(1).LockDoor();
                    charger.Received(1).StartCharge();
                }
            );
        }

    }
}
