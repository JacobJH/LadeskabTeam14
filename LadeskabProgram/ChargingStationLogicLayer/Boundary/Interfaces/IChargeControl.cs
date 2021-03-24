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

        // Findes der flere metoder / properties så skriv dem under her:

        public bool IsConnected();

    }
}
