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
        [Test]
        public void RfidDetected_StateIsAvalibleAndConnected_CallLockDoorOnDoorAndStartChargeOnChargeControl()
        {
            //Arrange
            charger.IsConnected().Returns(true);
            door.DoorIsLocked.Returns(false);
            door.DoorIsOpen.Returns(false);
            door.closeDoorEvent += Raise.EventWith(new DoorEventArgs(){EventDoorState = DoorState.Closed});

            //Act
            rfid.RFIDReaderEvent += Raise.EventWith(new RFIDDetectedArgs{});


            //Assert
            charger.Received(1).StartCharge();
            door.Received(1).LockDoor();
            //disp.Received(1).DisplayMessage(Arg.Any<string>());
        }

        [Test]
        public void RfidDetected_StateIsAvalibleButNotConnected_DispIsCalled()
        {
            //Arrange
            charger.IsConnected().Returns(false);
            door.DoorIsLocked.Returns(false);
            door.DoorIsOpen.Returns(false);
            door.closeDoorEvent += Raise.EventWith(new DoorEventArgs() { EventDoorState = DoorState.Closed });

            //Act
            rfid.RFIDReaderEvent += Raise.EventWith(new RFIDDetectedArgs { });


            //Assert
            charger.Received(0).StartCharge();
            door.Received(0).LockDoor();
            //disp.Received(1).DisplayMessage(Arg.Any<string>());
        }

        [Test]
        public void RfidDetected_StateIsopened_nothingIsCalled()
        {
            //Arrange
            charger.IsConnected().Returns(false);
            door.DoorIsLocked.Returns(false);
            door.DoorIsOpen.Returns(false);
            door.openDoorEvent += Raise.EventWith(new DoorEventArgs() { EventDoorState = DoorState.Opened });

            //Act
            rfid.RFIDReaderEvent += Raise.EventWith(new RFIDDetectedArgs { });


            //Assert
            charger.Received(0).StartCharge();
            charger.Received(0).StopCharge();
            door.Received(0).LockDoor();
            door.Received(0).UnlockDoor();
        }

        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(52)]
        [TestCase(529)]
        public void RfidDetected_StateIsLocked_UnlockIsCalledOnDoorAndStopChargeIsCalledOnCharger(int id)
        {
            //Arrange
            charger.IsConnected().Returns(true);
            door.DoorIsLocked.Returns(false);
            door.DoorIsOpen.Returns(false);
            door.closeDoorEvent += Raise.EventWith(new DoorEventArgs() { EventDoorState = DoorState.Closed });
            rfid.RFIDReaderEvent += Raise.EventWith(new RFIDDetectedArgs { IncomingRFIDFromScanner = id});


            //Act
            rfid.RFIDReaderEvent += Raise.EventWith(new RFIDDetectedArgs { IncomingRFIDFromScanner = id });


            //Assert
            charger.Received(1).StopCharge();
            door.Received(1).UnlockDoor();
            logger.Received(1).LogDoorUnLocked(Arg.Any<int>());
        }


        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(52)]
        [TestCase(529)]
        public void RfidDetected_StateIsLockedButRecivedWrongID_DoorShouldStillBeLockedAndChargerShouldStillcharge(int id)
        {
            //Arrange
            charger.IsConnected().Returns(true);
            door.DoorIsLocked.Returns(false);
            door.DoorIsOpen.Returns(false);
            door.closeDoorEvent += Raise.EventWith(new DoorEventArgs() { EventDoorState = DoorState.Closed });
            rfid.RFIDReaderEvent += Raise.EventWith(new RFIDDetectedArgs { IncomingRFIDFromScanner = id });


            //Act
            rfid.RFIDReaderEvent += Raise.EventWith(new RFIDDetectedArgs { IncomingRFIDFromScanner = id + 10 });


            //Assert
            charger.Received(0).StopCharge();
            door.Received(0).UnlockDoor();
            logger.Received(0).LogDoorUnLocked(Arg.Any<int>());
        }

        [Test]
        public void DoorOpened_EventArgIsOpening_ShouldCallDisplayWithTextTilsluttelefon()
        {
            //Act
            door.openDoorEvent += Raise.EventWith(new DoorEventArgs() { EventDoorState  = DoorState.Opened});

            //assert
            disp.Received(1).DisplayMessage("Tilslut telefon");
        }

        [Test]
        public void DoorOpened_EventArgIsclosed_ShouldCallDisplayWithTextFejlMedAtÅbneDøren()
        {
            //Act
            door.openDoorEvent += Raise.EventWith(new DoorEventArgs() { EventDoorState = DoorState.Closed });


            //assert
            disp.Received(1).DisplayMessage("Fejl, med at åbne døren");
        }

        [Test]
        public void DoorClosed_EventArgIsClosed_ShouldCallDisplayWithTextIndlæsRFID()
        {
            //Act
            door.closeDoorEvent += Raise.EventWith(new DoorEventArgs() { EventDoorState = DoorState.Closed });


            //assert
            disp.Received(1).DisplayMessage("Indlæs RFID");
        }

        [Test]
        public void DoorClosed_EventArgIsOpened_ShouldCallDisplayWithTextLadeskabTilgængeligForEnAndentelefon()
        {
            //Act
            door.closeDoorEvent += Raise.EventWith(new DoorEventArgs() { EventDoorState = DoorState.Closed });


            //assert
            disp.Received(1).DisplayMessage("Ladeskab tilgængelig for en anden telefon");
        }

    }
}
