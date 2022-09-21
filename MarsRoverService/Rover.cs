using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverService
{
    public class Rover : IRover
    {
        private const int _DEFAULT_X_VALUE = 0;
        private const int _DEFAULT_Y_VALUE = 0;
        private const char _DEFAULT_FACING = 'N'; 
        public int CurrentXCoordinate { get; set; }
        public int CurrentYCoordinate { get; set; }
        public char CurrentDirectionFacing { get; set; }
        
        public Rover()
        {
            CurrentXCoordinate = _DEFAULT_X_VALUE;
            CurrentYCoordinate = _DEFAULT_Y_VALUE;
            CurrentDirectionFacing = _DEFAULT_FACING;
        }

        public void SetRoverPosition(int xCoordinate, int yCoordinate, char direction)
        {
            CurrentXCoordinate = xCoordinate;
            CurrentYCoordinate = yCoordinate;
            CurrentDirectionFacing = direction;
        }

        public void MoveRover()
        {
            throw new System.NotImplementedException();
        }
    }
}