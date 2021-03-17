using System;
using EventArguments;
using LogicLayer.Boundary.Interfaces;

namespace LogicLayer.Boundary
{
    public class RFIDReader : IRFID
    {
        public event EventHandler<null> RFIDReaderEvent;



        public void OnRfidRead(int id)
        {
            RFIDReaderEvent?.Invoke(this, new CurrentEventArgs() { Current = this.CurrentValue });
        }
    }
}