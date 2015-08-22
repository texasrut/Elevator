using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorProj.Class
{
    public class ElevatorButtonPanel 
    {
        private readonly int _id;
        private readonly IElevatorController _ecc;

        public ElevatorButtonPanel(int id, IElevatorController elevatorControllerConection)
        {
            _ecc = elevatorControllerConection;
            _id = id;
            
        }

        public void NotifyControllerSelectedFloor(int floor)
        {
            _ecc.RequestStop(_id, floor);
        }
    }
}
