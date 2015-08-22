using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorProj.Class
{
    class FloorButtonPanel
    {
        public int Floor { get; set; }

        private readonly IElevatorController _ecc;                               

        public FloorButtonPanel(IElevatorController elevatorControllerConnection, int floor)
        {
            Floor = floor;
            _ecc = elevatorControllerConnection;
        }

        public void NotifyControllerRequestElevator(Direction dir)
        {
            _ecc.RequestDispatch(Floor, dir);
        }
        
    }
}
