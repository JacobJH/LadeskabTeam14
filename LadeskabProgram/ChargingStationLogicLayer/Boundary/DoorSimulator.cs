﻿using System;
using LogicLayer.Boundary.Interfaces;

namespace LogicLayer.Boundary
{
    public class DoorSimulator : IDoor
    {
        public DoorSimulator()
        {
        }

        public void UnlockDoor()
        {
            throw new NotImplementedException();
        }

        public void OnDoorOpen()
        {
            throw new NotImplementedException();
        }

        public void OnDoorClose()
        {
            throw new NotImplementedException();
        }

        public void LockDoor()
        {
            throw new NotImplementedException();
        }
    }
}