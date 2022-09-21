using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverService
{
    public class Rover : IRover
    {
        public int CurrentXCoordinate { get; set; }
        public int CurrentYCoordinate { get; set; }
        public string CurrentDirectionFacing { get; set; }

        public void GetCurrentCoordinates()
        {
            throw new System.NotImplementedException();
        }

        public void SetRoverPosition()
        {

        }

        public void MoveRover()
        {
            throw new System.NotImplementedException();
        }
    }
}