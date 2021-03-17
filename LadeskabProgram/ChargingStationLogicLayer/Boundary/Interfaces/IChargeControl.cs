using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LogicLayer.Boundary.Interfaces
{
    public interface IChargeControl
    {
        // De her metoder / properties findes i StationControl handoutet:
        public void StartCharge();

        public void StopCharge();

        public bool Connected { get; set; } 

        // Findes der flere metoder / properties så skriv dem under her:



    }
}
