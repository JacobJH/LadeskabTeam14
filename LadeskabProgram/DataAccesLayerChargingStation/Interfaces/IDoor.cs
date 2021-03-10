using System.Collections.Generic;
using System.Text;

namespace DataAccesLayerChargingStation.Interfaces
{
    public interface IDoor
    {
        public void LockDoor();
        
        public void UnlockDoor();

        public void DoorOpened();

        public void DoorClosed();
    }
}
