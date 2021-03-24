using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LogicLayer.Boundary.Interfaces
{
    public interface IChargeControl
    {
        public void StartCharge();

        public void StopCharge();

        public bool Connected { get; set; }

    }
}
