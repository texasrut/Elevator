using System.Collections.Generic;

namespace ElevatorProj.Class
{
    public class ElevatorController : IElevatorController
    {
        public IList<Elevator> Elevators { get; set; }
        private IDispatchCalculator _dc; 
        private IElevatorStopQueue _esp;    

        public ElevatorController(int totalfloors, int totalElevators)
        {
            _dc = new DispatchCalculator(totalfloors);
            _esp = new ElevatorBankQueues(totalElevators);
        }

        //calulate and send the most efficient elevator to destFloor
        public void RequestDispatch(int requestedFloor, Direction requestedDir)
        {
            //populate request
            DispatchRequest dr = new DispatchRequest(new List<Elevator>(Elevators), _esp.GetAllStopForBank(), requestedDir, requestedFloor);

            //Make Request
            _dc.CalculateDispatchRequests(dr);

            Direction directionToNextStop = GetDirectionToNextStop(dr.DispatchedElevatorPositionID,
                                                       dr.RequestedFloor);

            //TODO:Lock queue on elevatorID
            if (_esp.GetQueueCount(dr.DispatchedElevatorPositionID) == 0)
            {

                _esp.InsertStop(requestedFloor, directionToNextStop, dr.DispatchedElevatorPositionID);

                //TODO:unlock queue
                //TODO:Spawn Thread
                BeginRoute(dr.DispatchedElevatorPositionID);
            }
            else
            {
                _esp.InsertStop(requestedFloor, directionToNextStop, dr.DispatchedElevatorPositionID);
                //TODO:unlock queue
            }
        }

        //Send the elevator with matching id to destFloor
        public void RequestStop(int elevatorID, int requestedFloor)
        {
            Direction directionToNextStop = GetDirectionToNextStop(elevatorID, requestedFloor);            

           //Lock queue on elevator id
           if (_esp.GetQueueCount(elevatorID) == 0)
           {
               _esp.InsertStop(requestedFloor, directionToNextStop, elevatorID);
               //unlock queue
               //New Thread
               BeginRoute(elevatorID);
           }
           else
           {
               _esp.InsertStop(requestedFloor, directionToNextStop, elevatorID);
               //unlock queue
           }
        } 


        //move the elevator to then next destination until the queue is empty
        private void BeginRoute(int elevatorID)
        {
            int destination;
            
            //TODO:Lock Queue on ElevatorID
            while(_esp.GetQueueCount(elevatorID) > 0)
            {
                destination = _esp.Dequeue(elevatorID);
                //TODO:Unlock Queue
                Elevators[elevatorID].Move(destination - Elevators[elevatorID].CurFloor);
                //TODO:Lock Queue
            }
            //TODO:Unlock Queue
        }        

        private Direction GetDirectionToNextStop(int elevatorID, int requestedFloor)
        {
            var state = Elevators[elevatorID].CurState;
            Direction dir;


            if (state != State.Idle)
            {
                dir = state == State.MovingUp ? Direction.Up : Direction.Down;
            }
            else
            {
                dir = requestedFloor - Elevators[elevatorID].CurFloor > 0 ? Direction.Up : Direction.Down;
            }
            
            return dir;            
        }
    }


}
