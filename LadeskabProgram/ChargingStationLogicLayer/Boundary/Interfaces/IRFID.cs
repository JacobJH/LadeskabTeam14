using System;
using System.Collections.Generic;
using System.Text;
using EventArguments;

namespace LogicLayer.Boundary.Interfaces
{
    public interface IRFID
    {
        public event EventHandler<RFIDDetectedArgs> RFIDReaderEvent;

        public void OnRfidRead(int id);
    }
}
