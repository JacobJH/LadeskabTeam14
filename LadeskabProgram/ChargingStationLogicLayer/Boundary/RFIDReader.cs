using System;
using System.IO;
using EventArguments;
using LogicLayer.Boundary.Interfaces;

namespace LogicLayer.Boundary
{
    public class RFIDReader : IRFID
    {
        public event EventHandler<RFIDDetectedArgs> RFIDReaderEvent;


        public void OnRfidRead(int id)
        {
            RFIDReaderEvent?.Invoke(this, new RFIDDetectedArgs() { IncomingRFIDFromScanner = id });
        }
    }
}