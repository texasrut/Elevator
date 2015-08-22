using System;
using System.Collections.Generic;
namespace ElevatorProj.Class
{
    public interface IElevatorController
    {
        IList<Elevator> Elevators { get; set; }
        void RequestDispatch(int requestedFloor, Direction requestedDir);
        void RequestStop(int elevatorID, int requestedFloor);
    }
}
