using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorProj.Class
{
    class DispatchCalculator : IDispatchCalculator
    {
        private readonly int _totalFloors;

        public DispatchCalculator(int totalFloors)
        {
            this._totalFloors = totalFloors;    
        }
    
        //returns the most suitable elevator to fufil the request, based on the distance from the request, 
        //the number of pending requests, and the direction the elevator is currently travelling
        public void CalculateDispatchRequests(IDispatchRequest dr)
        {
            List<double> scores = new List<double>();

            int maxRequests = 0;
            foreach (var q in dr.StopQueues)
            {
                if (maxRequests > q.Count)
                    maxRequests = q.Count;
            }

            foreach (var e in  dr.Elevators)
            {
                //Calculate Distance from Stop Score
                float floorDif = e.CurFloor - dr.RequestedFloor;
                var distanceRatio = 1 - (Math.Sqrt(floorDif * floorDif)/_totalFloors);
                
                //Calculate Travelling Proper Direction Score
                float directionScore = 0;
                if (e.CurState == State.Idle)
                {
                    directionScore = 1;
                }
                else if (dr.RequestedDir == Direction.Down)
                {
                    if (floorDif > 0 && e.CurState == State.MovingDown)
                    {
                        directionScore = 1;
                    }                    
                }
                else if (dr.RequestedDir == Direction.Up)
                {
                    if (floorDif < 0 && e.CurState == State.MovingUp)
                    {
                        directionScore = 1;
                    }
                }


                
                //Calculate Request Backlog Score
                float backlogScore = maxRequests == 0 ? 0 : 1 - (dr.StopQueues[e.ID].Count / maxRequests);

                scores.Add(distanceRatio + directionScore + backlogScore);
            }

            var dispatchPos = scores.IndexOf(scores.Max());            
        }
    }
}
