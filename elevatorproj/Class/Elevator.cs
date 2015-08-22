using System;
using System.Threading;

namespace ElevatorProj.Class
{
    public class Elevator
    {
        public int CurFloor { get; set; }
        public int ID { get; set; }
        public State CurState { get; set; }
        public ElevatorButtonPanel EPB { get; set; } 

        public Elevator(IElevatorController elevatorControllerConnection, int elevatorID)
        {
            ID = elevatorID;
            CurFloor = 1;
            CurState = State.Idle;
            EPB = new ElevatorButtonPanel(elevatorID, elevatorControllerConnection);        
        }

        //move up or down 
        public void Move(int vector)
        {
            Console.WriteLine("Starting: " + CurFloor);

            Direction dir = vector > 0 ? Direction.Up : Direction.Down;
            
            for (int i = Math.Abs(vector); i > 0; i--)
            {
                if (dir == Direction.Up)
                {                    
                    CurFloor++;
                    CurState = State.MovingUp;
                }
                else
                {
                    CurFloor--;
                    CurState = State.MovingDown;
                }
                Console.WriteLine("NextFloor: " + CurFloor);
                //Simulate Time to next floor
                Thread.Sleep(1000);
            }

            Console.WriteLine("Stopping at " + CurFloor + '\n');
            CurState = State.Idle;       
        }
     }

    public enum State
    {
        MovingUp,
        MovingDown,
        Idle
    }

    public enum Direction
    { 
        Up,
        Down
    }
}
