using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventArguments;
using LogicLayer.Boundary;
using LogicLayer.Boundary.Interfaces;

namespace LogicLayer.Controllers
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        private enum LadeskabState
        {
            Available,
            Locked,
            DoorOpen
        };

        // Her mangler flere member variable
        private LadeskabState _state;
        private IChargeControl _charger;
        private int _oldId;
        private IDoor _door;
        private IDisplay _disp;
        private ILogger _logger;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        // Her mangler constructor
        


        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen

        public StationControl(IChargeControl charger, int oldId, IDoor door, IDisplay disp, ILogger logger)
        {
            _charger = charger;
            _oldId = oldId;
            _door = door;
            _disp = disp;
            _logger = logger;
        }


        #region EventHandlers

        //private void RfidDetected(int id)

        private void RfidDetected(object sender, RFIDDetectedArgs e)
        {
            int id = e.IncomingRFIDFromScanner;

            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        _logger.LogDoorLocked(id);
                        _disp.DisplayMessage("Skabet er låst og din telefon lades. Brug dit RFID tag til at låse op.");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _disp.DisplayMessage("Din telefon er ikke ordentlig tilsluttet. Prøv igen.");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        _logger.LogDoorUnLocked(id);
                        _disp.DisplayMessage("Tag din telefon ud af skabet og luk døren");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _disp.DisplayMessage("Forkert RFID tag");
                    }

                    break;
            }
        }

        //TODO mangler Tests - Lasse 

        private void DoorOpened(object sender, DoorEventArgs e)
        {
            if (e.EventDoorState == DoorState.Opened)
            {
                _state = LadeskabState.DoorOpen;
                _disp.DisplayMessage("Tilslut telefon");
            }
            else
            {
                _disp.DisplayMessage("Fejl, med at åbne døren");
            }
        }
        //TODO mangler Tests - Lasse 

        private void DoorClosed(object sender, DoorEventArgs e)
        {
            if (e.EventDoorState == DoorState.Opened)
            {
                _state = LadeskabState.Available;
                _disp.DisplayMessage("Indlæs RFID");
            }
        }
        //TODO mangler Tests  - Lasse 
        #endregion


    }
}
