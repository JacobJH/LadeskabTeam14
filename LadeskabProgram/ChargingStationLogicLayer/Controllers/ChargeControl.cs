using System;
using System.Collections.Generic;
using System.Text;
using LogicLayer.Boundary.Interfaces;

namespace LogicLayer.Controllers
{
    public class ChargeControl: IChargeControl
    {
        //TODO test og skriv færdig - Jacob 

        private IDisplay display;
        private IUsbCharger usbCharger;
        public ChargeControl(IDisplay display, IUsbCharger usbCharger)
        {
            this.display = display;
            this.usbCharger = usbCharger;
        }
        public void StartCharge()
        {
            usbCharger.StartCharge();
            display.DisplayMessage("Telefonen lader");
        }

        public void StopCharge()
        {
            usbCharger.StopCharge();
            display.DisplayMessage("Telefonon lader ikke");
        }

        public bool Connected { get; set; }
    }
}
