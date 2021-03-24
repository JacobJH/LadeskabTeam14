using System;
using EventArguments;
using LogicLayer.Boundary.Interfaces;

namespace LogicLayer.Boundary
{
    public class DoorSimulator : IDoor
    {
        public event EventHandler<DoorEventArgs> openDoorEvent;
        public event EventHandler<DoorEventArgs> closeDoorEvent;

        //BEMÆRK: her simuleres døren ved at udskrive på console vinduet - derfor anvendes displayet ikke til denne udskrift 

        public bool DoorIsOpen { get; private set; }
        public bool DoorIsLocked { get; private set; }

        public DoorSimulator()
        {
            DoorIsLocked = false;
            DoorIsOpen = false;
        }


        public void UnlockDoor()
        {
            if (DoorIsLocked && !DoorIsOpen)
            {
                DoorIsLocked = false;
                Console.WriteLine("Døren er nu låst op"); // simulering af åbning 
            }
            else
            {
                Console.WriteLine("Kan ikke låse døren op, hvis den ikke er lukket og låst"); // simulering af ulåst dør 
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
                    Console.WriteLine("Døren er låst og kan ikke åbnes"); // simulering af låst dør  
                }
            }
            else
            {
                Console.WriteLine("Kan ikke åbne døren, hvis den allerede er åben"); //simulering af åben dør 
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
                Console.WriteLine("Døren er nu låst"); //simulering af låse dør 
            }
            else
            {
                Console.WriteLine("Kan ikke låse døren, hvis den ikke er lukket og ulåst"); //simulering af ulåst dør 
            }
        }
    }
}