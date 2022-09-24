using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverService
{
    public class Rover : IRover //, IPlateau
    {
        private const int _DEFAULT_X_VALUE = 0;
        private const int _DEFAULT_Y_VALUE = 0;
        private const char _DEFAULT_FACING = 'N';
        private Plateau _plateau;
        public readonly Dictionary<char, Direction> _directionDict;
        public readonly Dictionary<char, RoverCommand> _instructionDict;

        public int GridMaxXCoordinate { get; set; }
        public int GridMaxYCoordinate { get; set; }
        public int GridStartXCoordinate { get; set; }
        public int GridStartYCoordinate { get; set; }

        public int CurrentXCoordinate { get; set; }
        public int CurrentYCoordinate { get; set; }
        public Direction CurrentDirectionFacing { get; set; }
               
        public Rover(Plateau plateau)
        {
            //Initialising plateau and assigning value to it
            _plateau = new Plateau();
            _plateau = plateau;

            //Assigning the coordinates of the plateau to the Rover class's local variables
            GridMaxXCoordinate = _plateau.GridMaxXCoordinate;
            GridMaxYCoordinate = _plateau.GridMaxYCoordinate;
            GridStartXCoordinate = _plateau.GridStartXCoordinate;
            GridStartYCoordinate = _plateau.GridStartYCoordinate;

            //Dictionary to map Directions to the Acronyms
            _directionDict = new Dictionary<char, Direction>()
            {
                { 'N', Direction.North },
                { 'S', Direction.South },
                { 'W', Direction.West },
                { 'E', Direction.East }
            };

            //Dictionary to map Instructions to the Acronyms
            _instructionDict = new Dictionary<char, RoverCommand>()
            {
                { 'L', RoverCommand.TurnLeft },
                { 'R', RoverCommand.TurnRight },
                { 'M', RoverCommand.MoveForward }
            };
        }

        /// <summary>
        /// Method to set the Rover's position on the plateau
        /// </summary>
        /// <param name="xCoordinate"></param>
        /// <param name="yCoordinate"></param>
        /// <param name="direction"></param>
        public void SetRoverPosition(int xCoordinate, int yCoordinate, char direction)
        {
            CurrentXCoordinate = xCoordinate;
            CurrentYCoordinate = yCoordinate;
            CurrentDirectionFacing = _directionDict[direction];

            ValidateRoverPositionOnThePlateau(GridMaxXCoordinate, GridMaxYCoordinate, xCoordinate, yCoordinate);
        }

        /// <summary>
        /// Method to validate the Rover's position on the plateau
        /// </summary>
        /// <param name="gridMaxXCoordinate"></param>
        /// <param name="gridMaxYCoordinate"></param>
        /// <param name="xCoordinate"></param>
        /// <param name="yCoordinate"></param>
        /// <exception cref="ArgumentException"></exception>
        public void ValidateRoverPositionOnThePlateau(int gridMaxXCoordinate, int gridMaxYCoordinate,
            int xCoordinate, int yCoordinate)
        {
            var xValueOfRover = xCoordinate >= 0 && xCoordinate <= gridMaxXCoordinate;
            var yValueOfRover = yCoordinate >= 0 && yCoordinate <= gridMaxYCoordinate;

            if (!(xValueOfRover && yValueOfRover))
                throw new ArgumentException("Rover position should not be outside the plateau grid.");
        }        
    }
}