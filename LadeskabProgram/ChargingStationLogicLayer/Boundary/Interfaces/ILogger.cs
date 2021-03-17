using System.Collections.Generic;
using System.Text;

namespace LogicLayer.Boundary.Interfaces
{
    public interface ILogger
    {
        public void LogDoorLocked(int id);
        public void LogDoorUnLocked(int id);

    }
}
