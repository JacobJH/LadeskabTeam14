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

            usbConnectedArgs = null;

            uut.isConnectedEvent += (o, args) =>
            {
                usbConnectedArgs = args;
            };
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
