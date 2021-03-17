using System;
using System.Collections.Generic;
using System.Text;

namespace EventArguments
{

    public enum DoorState
    {
        Closed,
        Opened
    }

    public class DoorEventArgs: EventArgs
    {
        public DoorState EventDoorState { get; set; }
    }
}
