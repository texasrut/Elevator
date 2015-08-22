using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElevatorProj.Class;

namespace ElevatorProj
{
    class Program
    {
        static void Main(string[] args)
        {
            ElevatorBank eb = new ElevatorBank(6, 4);
            eb.SendElevatorToFloorFromWithinTheElevator(1, 3);
            eb.CallElevatorToFloorFromHallway(3, Direction.Down);                 
        }
    }
}
