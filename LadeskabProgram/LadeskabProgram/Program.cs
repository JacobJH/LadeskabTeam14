using System;
using System.Threading.Channels;
using LogicLayer.Boundary;
using LogicLayer.Boundary.Interfaces;
using LogicLayer.Controllers;

class Program
{
    static void Main(string[] args)
    {
		// Assemble your system here from all the classes
        IDoor door = new DoorSimulator();
        IRFID rfidReader = new RFIDReader();
        ILogger fileLogger = new FileLogger();
        IUsbCharger usbCharger = new UsbChargerSimulator();
        IDisplay display = new ConsoleDisplay();
        
        IChargeControl chargeControl = new ChargeControl(display,usbCharger);

        StationControl StationController = new StationControl(chargeControl,door,display,fileLogger,rfidReader);


        Console.WriteLine("E for Exit, O for opening the door, C for closing the door, R to read an RFID-tag");
        Console.WriteLine("K for connecting phone to charger, L for disconnecting phone from charger");
        bool finish = false;
        do
        {
            System.Console.WriteLine("Indtast E, O, C, R, K, L: ");
            string input;

            input = Console.ReadLine();
            if (string.IsNullOrEmpty(input)) continue;

            switch (input[0])
            {
                case 'E':
                    finish = true;
                    break;

                case 'O':
                    door.OnDoorOpen();
                    break;

                case 'C':
                    door.OnDoorClose();
                    break;

                case 'R':
                    System.Console.WriteLine("Indtast RFID id: ");
                    string idString = System.Console.ReadLine();

                    int id = Convert.ToInt32(idString);
                    rfidReader.OnRfidRead(id);
                    break;

                case 'K':
                    if (door.DoorIsOpen && !usbCharger.Connected)
                    {
                        usbCharger.SimulateConnected(true);
                        Console.WriteLine("Telefon tilsluttet");
                    }
                    else
                    {
                        Console.WriteLine("Laderen er optaget ellers er døren ikke åben");
                    }
                    break;

                case 'L':
                    if (door.DoorIsOpen && usbCharger.Connected)
                    {
                        usbCharger.SimulateConnected(false);
                        Console.WriteLine("Telefon taget ud af charger");
                    }
                    else
                    {
                        Console.WriteLine("Døren skal være åben og laderen optaget før du kan tage den ud af opladning");
                    }
                    break;

                default:
                    break;
            }

        } while (!finish);
    }
}