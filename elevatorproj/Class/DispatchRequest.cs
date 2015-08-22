using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorProj.Class
{
    public class DispatchRequest : ElevatorProj.Class.IDispatchRequest
    {
        public IList<Elevator> Elevators { get; set; }
        public List<List<int>> StopQueues { get; set; }
        public Direction RequestedDir { get; set; }
        public int RequestedFloor { get; set; }

        public int DispatchedElevatorPositionID { get; set; } //Value populated after calculation

        public DispatchRequest(List<Elevator> elevators, List<List<int>> stopQueues, Direction requestedDir, int requestedFloor)
        {
            Elevators = elevators;
            StopQueues = stopQueues;
            RequestedDir = requestedDir;
            RequestedFloor = requestedFloor;

        }
    }
}
