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
        private Plateau _plateau;
        public int CurrentXCoordinate { get; set; }
        public int CurrentYCoordinate { get; set; }
        public char CurrentDirectionFacing { get; set; }
        
        public Rover()
        {
            _plateau = new Plateau();
           /* CurrentXCoordinate = _DEFAULT_X_VALUE;
            CurrentYCoordinate = _DEFAULT_Y_VALUE;
            CurrentDirectionFacing = _DEFAULT_FACING;
           */
        }

        public void SetRoverPosition(int gridMaxXCoordinate, int gridMaxYCoordinate, 
            int xCoordinate, int yCoordinate, char direction)
        {
            CurrentXCoordinate = xCoordinate;
            CurrentYCoordinate = yCoordinate;
            CurrentDirectionFacing = direction;

            ValidateRoverPositionOnThePlateau(gridMaxXCoordinate, gridMaxYCoordinate, xCoordinate, yCoordinate);

        }
        public void ValidateRoverPositionOnThePlateau(int gridMaxXCoordinate, int gridMaxYCoordinate, int xCoordinate, int yCoordinate)
        {
            var xValueOfRover = xCoordinate >= 0 && xCoordinate <= gridMaxXCoordinate;
            var yValueOfRover = yCoordinate >= 0 && yCoordinate <= gridMaxYCoordinate;

            if (!(xValueOfRover && yValueOfRover))
                throw new ArgumentException("Rover position should not be outside the plateau grid.");

        }

        public void MoveRover()
        {
            throw new System.NotImplementedException();
        }
    }
}