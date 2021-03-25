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

            display.Received(1).DisplayMessage("Telefonon lader ikke");
        }

        //InteractionTest
        [Test]
        public void NewChargeHandler_GetCurretBelowOrEqualTo0_DosentCallDisplay()
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


        [TestCase(6)]
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

        [TestCase(600)]
        public void NewChargeHandler_GetCurretAtAbove500_CallsDisplay1TimeAndCallsStopChargeOnUsbCharger(double current)
        {
            //Arrange
            IDisplay disp = Substitute.For<IDisplay>();
            IUsbCharger usb = Substitute.For<IUsbCharger>();

            ChargeControl uut = new ChargeControl(disp, usb);

            //Act
            usb.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs() { Current = current });


            //Assert
           // disp.Received(1).DisplayMessage(Arg.Any<string>());

            usb.Received(1).StopCharge();
        }




        //[Test]
        //public void Eventstuff()
        //{
        //    //act
        //    uut.IsConnected();

        //    //assert
        //    Assert
        //}
    }
}
