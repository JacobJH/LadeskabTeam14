using System;
using System.Collections.Generic;
using System.Text;
using EventArguments;

namespace LogicLayer.Boundary.Interfaces
{
    public interface IDoor
    {
        public event EventHandler<DoorEventArgs> openDoorEvent;
        public event EventHandler<DoorEventArgs> closeDoorEvent;

        public bool DoorIsOpen { get; }
        public bool DoorIsLocked { get; }

        public void LockDoor();
        
        public void UnlockDoor();

        public void OnDoorOpen();

        public void OnDoorClose();
    }
}
