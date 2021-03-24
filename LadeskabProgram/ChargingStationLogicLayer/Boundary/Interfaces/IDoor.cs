using System.Collections.Generic;
using System.Text;

namespace LogicLayer.Boundary.Interfaces
{
    public interface IDoor
    {
        public bool DoorIsOpen { get; }
        public bool DoorIsLocked { get; }

        public void LockDoor();
        
        public void UnlockDoor();

        public void OnDoorOpen();

        public void OnDoorClose();
    }
}
