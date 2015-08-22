using System;
using System.Collections.Generic;
namespace ElevatorProj.Class
{
    interface IElevatorStopQueue
    {
        int Count { get; }
        int Dequeue(int elevatorID);
        int GetQueueCount(int elevatorID);
        List<List<int>> GetAllStopForBank();
        void InsertStop(int requestedFloor, Direction directionToNextStop, int elevatorID);
    }
}
