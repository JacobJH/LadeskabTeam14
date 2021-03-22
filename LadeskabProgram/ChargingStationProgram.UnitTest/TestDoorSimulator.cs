using System;
using System.Collections.Generic;
using System.Text;
using EventArguments;
using LogicLayer.Boundary;
using NUnit.Framework;

namespace ChargingStationProgram.UnitTest
{
    [TestFixture]
    public class TestDoorSimulator
    {
        private DoorSimulator uut;
        private DoorEventArgs openDoorArgs;
        private DoorEventArgs closeDoorArgs;

        // TODO spørg de andre om hvordan man tester metoderne til at låse/låse op for døren

        [SetUp]
        public void Setup()
        {
            //arrange 
            uut = new DoorSimulator(); //Door closed and unlocked by default

            openDoorArgs = null;
            closeDoorArgs = null;

            uut.closeDoorEvent += (o, args) =>
            {
                closeDoorArgs = args;
            };

            uut.openDoorEvent += (o, args) =>
            {
                openDoorArgs = args;
            };
        }

        [Test]
        public void OnDoorClosed_ClosedAndUnlocked_NoEventTriggers()
        {
            //act
            uut.OnDoorClose();

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(openDoorArgs, Is.Null);
                Assert.That(closeDoorArgs, Is.Null);

            });
        }

        [Test]
        public void OnDoorClosed_ClosedAndLocked_NoEventTriggers()
        {
            //arrange
            uut.LockDoor();

            //act

            uut.OnDoorClose();

            //assert
            //assert
            Assert.Multiple(() =>
            {
                Assert.That(openDoorArgs, Is.Null);
                Assert.That(closeDoorArgs, Is.Null);

            });
        }

        [Test]
        public void OnDoorClosed_Opened_CloseDoorEventTriggersWithDoorStateClosed()
        {
            //arrange
            uut.OnDoorOpen();
            openDoorArgs = null;


            //act

            uut.OnDoorClose();

            //assert
            //assert
            Assert.Multiple(() =>
            {
                Assert.That(openDoorArgs, Is.Null);
                Assert.That(closeDoorArgs, Is.Not.Null);
                Assert.That(closeDoorArgs.EventDoorState, Is.EqualTo(EventArguments.DoorState.Closed));

            });
        }


        [Test]
        public void OnDoorOpened_ClosedAndUnlocked_OpenDoorEventTriggersWithDoorStateOpened()
        {
            //act
            uut.OnDoorOpen();

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(closeDoorArgs, Is.Null);
                Assert.That(openDoorArgs, Is.Not.Null);
                Assert.That(openDoorArgs.EventDoorState, Is.EqualTo(EventArguments.DoorState.Opened));

            });
        }


        [Test]
        public void OnDoorOpened_ClosedAndLocked_NoEventTriggers()
        {
            //arrange
            uut.LockDoor();

            //act
            uut.OnDoorOpen();

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(closeDoorArgs, Is.Null);
                Assert.That(openDoorArgs, Is.Null);
            });
        }



        [Test]
        public void OnDoorOpened_DoorAlreadyOpened_NoEventTriggers()
        {
            //arrange
            uut.OnDoorOpen();
            openDoorArgs = null;


            //act
            uut.OnDoorOpen();

            //assert
            Assert.Multiple(() =>
            {
                Assert.That(closeDoorArgs, Is.Null);
                Assert.That(openDoorArgs, Is.Null);
            });
        }
    }
}
