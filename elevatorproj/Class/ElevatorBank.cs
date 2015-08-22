using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorProj.Class
{
    class ElevatorBank
    {
        private ElevatorController _elevatorController;
        private IList<Elevator> _elevators;
        private IList<FloorButtonPanel> _floorButtonPanels;        

        public ElevatorBank(int totalFloors, int totalElevators)
        {
             _elevatorController = new ElevatorController(totalFloors, totalElevators);

            _floorButtonPanels = new List<FloorButtonPanel>();
            for (int i = 0; i < totalFloors; i++)
            {
                var d = new FloorButtonPanel(_elevatorController, i + 1);
                _floorButtonPanels.Add(d);
            }

            _elevators = new List<Elevator>();
            for (int i = 0; i < totalElevators; i++)
            {
                _elevators.Add(new Elevator(_elevatorController, i));
            }
            _elevatorController.Elevators = _elevators;
        }

        public void CallElevatorToFloorFromHallway(int floor, Direction dir)
        {
            _floorButtonPanels[floor - 1].NotifyControllerRequestElevator(dir);        
        }


        public void SendElevatorToFloorFromWithinTheElevator(int elevator, int floor)
        {
            _elevators[elevator - 1].EPB.NotifyControllerSelectedFloor(floor);
        }
    }
}
