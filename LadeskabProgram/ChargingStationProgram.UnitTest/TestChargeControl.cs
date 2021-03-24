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
            Assert.Multiple(() =>
            {
                usbCharger.Received(1).StartCharge();
                display.Received(1).DisplayMessage("Telefonen lader");

            });
        }
        [Test]
        public void StopCharge_Call_Sent()
        {
            //act
            uut.StopCharge();

            //assert
            Assert.Multiple(() =>
            {
                usbCharger.Received(1).StopCharge();
                display.Received(1).DisplayMessage("Telefonon lader ikke");

            });
        }
    }
}
