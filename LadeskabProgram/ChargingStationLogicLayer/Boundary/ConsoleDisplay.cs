using System;
using System.Collections.Generic;
using System.Text;
using LogicLayer.Boundary.Interfaces;

namespace LogicLayer.Boundary
{
    public class ConsoleDisplay : IDisplay
    {
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
