using System;
using System.Collections.Generic;
namespace ElevatorProj.Class
{
    interface IDispatchRequest
    {
        int DispatchedElevatorPositionID { get; set; }
        IList<Elevator> Elevators { get; set; }
        Direction RequestedDir { get; set; }
        int RequestedFloor { get; set; }
        List<List<int>> StopQueues { get; set; }
    }
}
