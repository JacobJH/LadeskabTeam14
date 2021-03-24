using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
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
        public void DoorStatus_IsClosedAndUnlocked_ByDefaultNoEventTriggered()
        {
            //act

            //Assert

            Assert.Multiple(() =>
            {
                Assert.That(openDoorArgs, Is.Null);
                Assert.That(closeDoorArgs, Is.Null);
                Assert.That(uut.DoorIsLocked,Is.False);
                Assert.That(uut.DoorIsOpen, Is.False);
            });
        }

        #region TestOfDoorStatus

        [Test]
        public void DoorStatus_IsOpened_DoorIsOpenIsTrue()
        {
            //act
            uut.OnDoorOpen();

            //Assert

            Assert.Multiple(() =>
            {
                Assert.That(uut.DoorIsLocked, Is.False);
                Assert.That(uut.DoorIsOpen, Is.True);
            });
        }

        [Test]
        public void DoorStatus_IsUnlocked_DoorIsLockedIsFalse()
        {
            //act
            uut.UnlockDoor();

            //Assert

            Assert.Multiple(() =>
            {
                Assert.That(uut.DoorIsLocked, Is.False);
                Assert.That(uut.DoorIsOpen, Is.False);
            });
        }


        [Test]
        public void DoorStatus_IsOpenAndUnlocked_DoorIsOpenIsTrue()
        {
            //act
            uut.OnDoorOpen();
            uut.UnlockDoor();

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(uut.DoorIsLocked, Is.False);
                Assert.That(uut.DoorIsOpen, Is.True);
            });
        }

        [Test]
        public void DoorStatus_IsClosedAndLocked_DoorIsLockedIsTrue()
        {
            //act
            uut.LockDoor();

            //Assert

            Assert.Multiple(() =>
            {
                Assert.That(uut.DoorIsLocked, Is.True);
                Assert.That(uut.DoorIsOpen, Is.False);
            });
        }

        [Test]
        public void DoorStatus_IsLockedAndUnlocked_DoorIsLockedIsFalse()
        {
            //act
            uut.LockDoor();
            uut.UnlockDoor();
            //Assert

            Assert.Multiple(() =>
            {
                Assert.That(uut.DoorIsLocked, Is.False);
                Assert.That(uut.DoorIsOpen, Is.False);
            });
        }

        [Test]
        public void DoorStatus_IsLockedAndOpened_DoorIsLockedIsTrueAndDoorIsClosed()
        {
            //act
            uut.LockDoor();
            uut.OnDoorOpen();
            //Assert

            Assert.Multiple(() =>
            {
                Assert.That(uut.DoorIsLocked, Is.True);
                Assert.That(uut.DoorIsOpen, Is.False);
            });
        }

        [Test]
        public void DoorStatus_IsOpenedAndLocked_DoorIsOpenAndNotLocked()
        {
            //act
            uut.OnDoorOpen();
            uut.LockDoor();
            //Assert

            Assert.Multiple(() =>
            {
                Assert.That(uut.DoorIsLocked, Is.False);
                Assert.That(uut.DoorIsOpen, Is.True);
            });
        }


        #endregion

        #region TestOfDoorEvents

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
        #endregion
    }
}
