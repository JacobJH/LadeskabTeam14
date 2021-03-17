using System.Collections.Generic;
using System.Text;

namespace LogicLayer.Boundary.Interfaces
{
    public interface IRFID
    {
        public void OnRfidRead(int id);
    }
}
