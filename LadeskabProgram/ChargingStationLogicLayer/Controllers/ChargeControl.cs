using System;
using System.Collections.Generic;
using System.Text;
using LogicLayer.Boundary.Interfaces;

namespace LogicLayer.Controllers
{
    public class ChargeControl: IChargeControl
    {
        public void StartCharge()
        {
            throw new NotImplementedException();
        }

        public void StopCharge()
        {
            throw new NotImplementedException();
        }

        public bool Connected { get; set; }
    }
}
