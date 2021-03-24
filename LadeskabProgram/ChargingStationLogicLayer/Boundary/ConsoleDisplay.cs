using System;
using System.Collections.Generic;
using System.Text;
using LogicLayer.Boundary.Interfaces;

namespace LogicLayer.Boundary
{
    public class ConsoleDisplay : IDisplay
    {
        // Simulering af udskrift på displayet gøres i console vinduet da der ikke er hardware til rådighed - bemærk at der er interface tilgængeligt til senere implementering af hardware 
    
        public void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}
