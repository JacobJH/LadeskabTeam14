using System;
using EventArguments;
using LogicLayer.Boundary.Interfaces;

namespace LogicLayer.Boundary
{
    public class DoorSimulator : IDoor
    {
        public event EventHandler<DoorEventArgs> openDoorEvent;
        public event EventHandler<DoorEventArgs> closeDoorEvent;

        private bool DoorIsOpen;
        private bool DoorIsLocked;

        public DoorSimulator()
        {
            DoorIsLocked = false;
            DoorIsOpen = false;
        }
        //TODO Skriv kode til de resterende Notimplemented ting. -Lars
        //TODO Skriv tests til metoderne i DoorSimulator - Lars
        public void UnlockDoor()
        {
            if (DoorIsLocked && !DoorIsOpen)
            {
                DoorIsLocked = false;
                Console.WriteLine("Døren er nu låst op");
            }
            else
            {
                Console.WriteLine("Kan ikke låse døren op, hvis den ikke er lukket og låst");
            }
        }

        public void OnDoorOpen()
        {
            if (!DoorIsOpen)
            {
                if (!DoorIsLocked)
                {
                    openDoorEvent?.Invoke(this, new DoorEventArgs() { EventDoorState = DoorState.Opened });
                    DoorIsOpen = true;
                }
                else
                {
                    Console.WriteLine("Døren er låst og kan ikke åbnes");
                }
            }
            else
            {
                Console.WriteLine("Kan ikke åbne døren, hvis den allerede er åben");
            }
            
        }

        public void OnDoorClose()
        {
            if (DoorIsOpen)
            {
                closeDoorEvent?.Invoke(this, new DoorEventArgs() { EventDoorState = DoorState.Closed });
                DoorIsOpen = false;
            }
            
        }

        public void LockDoor()
        {
            if (!DoorIsLocked && !DoorIsOpen)
            {
                DoorIsLocked = true;
                Console.WriteLine("Døren er nu låst");
            }
            else
            {
                Console.WriteLine("Kan ikke låse døren, hvis den ikke er lukket og ulåst");
            }
        }
    }
}