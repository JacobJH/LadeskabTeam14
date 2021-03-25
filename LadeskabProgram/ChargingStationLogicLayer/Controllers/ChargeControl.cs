using System;
using System.Collections.Generic;
using System.Text;
using EventArguments;
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

            usbCharger.CurrentValueEvent += NewChargeHandler;
        }


        public void NewChargeHandler(object sender, CurrentEventArgs e)
        {
            double current = e.Current;

            if (current  <= 0)
            {
            }
            else if (current <= 5)
            {
                display.DisplayMessage("Telefonen er fuldt opladt");
            }else if (current <= 500)
            {
                display.DisplayMessage("Oplader");
            }else if(current > 500)
            {
                display.DisplayMessage("fejl i opladning");
                StopCharge();
            }
        }


        public void StartCharge()
        {
            usbCharger.StartCharge();
            display.DisplayMessage("Telefonen lader");
        }

        public void StopCharge()
        {
            usbCharger.StopCharge();
            display.DisplayMessage("Telefonen lader ikke");
        }

        public bool IsConnected()
        {
            return usbCharger.Connected;
        }
    }
}
