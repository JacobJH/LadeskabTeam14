using System;
using EventArguments;
using LogicLayer.Boundary.Interfaces;

namespace LogicLayer.Boundary
{
    public class DoorSimulator : IDoor
    {
        public event EventHandler<DoorEventArgs> openDoorEvent;
        public event EventHandler<DoorEventArgs> closeDoorEvent;

        public DoorSimulator()
        {
        }

        public void UnlockDoor()
        {
            throw new NotImplementedException();
        }

        public void OnDoorOpen()
        {
            openDoorEvent?.Invoke(this, new DoorEventArgs() { EventDoorState = DoorState.Opened });
        }

        public void OnDoorClose()
        {
            closeDoorEvent?.Invoke(this, new DoorEventArgs() { EventDoorState = DoorState.Closed });
        }

        public void LockDoor()
        {
            throw new NotImplementedException();
        }
    }
}