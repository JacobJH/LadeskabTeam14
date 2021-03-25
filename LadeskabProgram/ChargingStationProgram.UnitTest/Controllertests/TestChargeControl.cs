using System;
using System.Collections.Generic;
using System.Text;
using EventArguments;
using LogicLayer.Controllers;
using NUnit.Framework;
using LogicLayer.Boundary.Interfaces;
using LogicLayer.Boundary;
using NSubstitute;

namespace ChargingStationProgram.UnitTest
{
    [TestFixture]
    public class TestChargeControl
    {
        private ChargeControl uut;
        private IDisplay display;
        private IUsbCharger usbCharger;
        private USBConnectedEventArgs usbConnectedArgs;
        [SetUp]
        public void Setup()
        {
            //arrange 
            display = Substitute.For<IDisplay>();
            usbCharger = Substitute.For<IUsbCharger>();
            uut = new ChargeControl(display, usbCharger);
        }
        [Test]
        public void StartCharge_Call_Sent()
        {
            //act
            uut.StartCharge();

            //assert
            usbCharger.Received(1).StartCharge();
        }
        [Test]
        public void StartCharge_DisplayCall_Sent()
        {
            //act
            uut.StartCharge();

            //assert

            display.Received(1).DisplayMessage("Telefonen lader");
        }
        [Test]
        public void StopCharge_Call_Sent()
        {
            //act
            uut.StopCharge();

            //assert
            usbCharger.Received(1).StopCharge();

        }
        [Test]
        public void StopCharge_DisplayCall_Sent()
        {
            //act
            uut.StopCharge();

            //assert
            display.Received(1).DisplayMessage("Telefonen lader ikke");
        }


        //InteractionTest
        [TestCase(-0.0001)]
        [TestCase(double.MinValue)]
        public void NewChargeHandler_GetCurretBelow0_ErrorThrown(double current)
        {
            //Arrange
            IDisplay disp = Substitute.For<IDisplay>();
            IUsbCharger usb = Substitute.For<IUsbCharger>();

            ChargeControl uut = new ChargeControl(disp, usb);



            //Assert
            Assert.Catch<ArgumentOutOfRangeException>(() =>
            {
                usb.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });
            });
        }

        //InteractionTest
        [Test]
        public void NewChargeHandler_GetCurretEqualTo0_DosentCallDisplay()
        {
            //Arrange
            IDisplay disp = Substitute.For<IDisplay>();
            IUsbCharger usb = Substitute.For<IUsbCharger>();

             ChargeControl uut = new ChargeControl(disp, usb);

             //Act
            usb.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = 0});


            //Assert
            disp.Received(0).DisplayMessage(Arg.Any<string>());
        }

        [TestCase(0.0001)]
        [TestCase(2.5)]
        [TestCase(5)]
        public void NewChargeHandler_GetCurretAtBelowOrEqualTo5AndAbove5_CallsDisplay1Time(double current)
        {
            //Arrange
            IDisplay disp = Substitute.For<IDisplay>();
            IUsbCharger usb = Substitute.For<IUsbCharger>();

            ChargeControl uut = new ChargeControl(disp, usb);

            //Act
            usb.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });


            //Assert
            disp.Received(1).DisplayMessage(Arg.Any<string>());
        }


        [TestCase(5.0001)]
        [TestCase(6)]
        [TestCase(250)]
        [TestCase(499)]
        [TestCase(500)]
        public void NewChargeHandler_GetCurretAtBelowOrEqualTo500AndAbove5_CallsDisplay1Time(double current)
        {
            //Arrange
            IDisplay disp = Substitute.For<IDisplay>();
            IUsbCharger usb = Substitute.For<IUsbCharger>();

            ChargeControl uut = new ChargeControl(disp, usb);

            //Act
            usb.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });


            //Assert
            disp.Received(1).DisplayMessage(Arg.Any<string>());
        }

        [TestCase(500.0001)]
        [TestCase(1000)]
        [TestCase(1000000)]
        [TestCase(double.MaxValue)]
        public void NewChargeHandler_GetCurretAtAbove500_CallsDisplay1TimeAndCallsStopChargeOnUsbCharger(double current)
        {
            //Arrange
            IDisplay disp = Substitute.For<IDisplay>();
            IUsbCharger usb = Substitute.For<IUsbCharger>();

            ChargeControl uut = new ChargeControl(disp, usb);

            //Act
            usb.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });


            //Assert
            disp.Received().DisplayMessage(Arg.Any<string>());

            usb.Received(1).StopCharge();
        }


        [Test]
        public void IsConnected_RecivedTrue_ConnectedIsTrue()
        {
            //Arrange
            IDisplay disp = Substitute.For<IDisplay>();
            IUsbCharger usb = Substitute.For<IUsbCharger>();
            ChargeControl uut = new ChargeControl(disp, usb);

            usb.Connected.Returns(true);

            //ACT
            bool result = uut.IsConnected();


            //Assert
            Assert.That(result, Is.True);
        }
        [Test]
        public void IsConnected_RecivedFalse_ConnectedIsfalse()
        {
            //Arrange
            IDisplay disp = Substitute.For<IDisplay>();
            IUsbCharger usb = Substitute.For<IUsbCharger>();
            ChargeControl uut = new ChargeControl(disp, usb);

            usb.Connected.Returns(false);

            //ACT
            bool result = uut.IsConnected();


            //Assert
            Assert.That(result, Is.False);
        }
    }
}
