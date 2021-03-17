using System;
using System.Collections.Generic;
using System.Text;

namespace EventArguments
{
    public class RFIDDetectedArgs : EventArgs
    {
        public int IncomingRFIDFromScanner { get; set; }
    }
}
