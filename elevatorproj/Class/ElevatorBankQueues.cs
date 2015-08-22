using System.Collections.Generic;
using System.Linq;

namespace ElevatorProj.Class
{
    class ElevatorBankQueues : IElevatorStopQueue
    {
        private List<List<int>> _stopQueues;

        public int Count
        {
            get
            {
                return _stopQueues.Count;
            }
        }

        public ElevatorBankQueues(int totalElevators)
        {
            _stopQueues = new List<List<int>>();
            for (int i = 0; i < totalElevators; i++)
            {
                _stopQueues.Add(new List<int>());
            }
        }

        //Get a count of an individual queue
        public int GetQueueCount(int elevatorID)
        {
            return _stopQueues[elevatorID].Count;
        }

        //Get the current state of the queues
        public List<List<int>> GetAllStopForBank()
        {
            //TODO:Deep Copy
            return _stopQueues;
        }

        //return and remove the first elemeent
        public int Dequeue(int elevatorID)
        {
            int rc = _stopQueues[elevatorID].First();
            _stopQueues[elevatorID].RemoveAt(0);
            return rc;
        }

        //insert a stop into the queue
        //The queue will insert according to the direction it is travelling,
        //if a stop is inserted in the opposite direction it will be inserted at the end of the queue
        public void InsertStop(int requestedFloor, Direction directionToNextStop, int elevatorID)
        {
            //Lock Queue on ID
            if (_stopQueues[elevatorID].Count > 0)
            {
                //If Stopdoesn't already exist
                if (!_stopQueues[elevatorID].Contains(requestedFloor))
                {
                    //unused
                    //int first = _stopQueues[elevatorID].First();
                    
                    
                    if (directionToNextStop == Direction.Up)
                    {
                        int maxpos = _stopQueues[elevatorID].IndexOf(_stopQueues[elevatorID].Max());
                        if (requestedFloor > _stopQueues[elevatorID].First())
                        {
                            int i = 1;
                            while (i < _stopQueues[elevatorID].Count && i <= maxpos && requestedFloor > _stopQueues[elevatorID][i])
                            {
                                i++;
                            }
                            _stopQueues[elevatorID].Insert(i, requestedFloor);
                        }
                        else
                        {
                            int i = maxpos + 1;
                            while (i < _stopQueues[elevatorID].Count && requestedFloor < _stopQueues[elevatorID][i])
                            {
                                i++;
                            }
                            _stopQueues[elevatorID].Insert(i, requestedFloor);
                        }
                    }
                    else
                    {
                        int minpos = _stopQueues[elevatorID].IndexOf(_stopQueues[elevatorID].Min());
                        if (requestedFloor < _stopQueues[elevatorID].First())
                        {
                            int i = 1;
                            while (i < _stopQueues[elevatorID].Count && i <= minpos && requestedFloor < _stopQueues[elevatorID][i])
                            {
                                i++;
                            }
                            _stopQueues[elevatorID].Insert(i, requestedFloor);
                        }
                        else
                        {
                            int i = minpos + 1;
                            while (i <= _stopQueues[elevatorID].Count - 1 && requestedFloor > _stopQueues[elevatorID][i])
                            {
                                i++;
                            }
                            _stopQueues[elevatorID].Insert(i, requestedFloor);
                        }
                    }
                }
            }
            else
            {
                _stopQueues[elevatorID].Add(requestedFloor);
            }
            //Unlock Queuue

        }
    }
}
