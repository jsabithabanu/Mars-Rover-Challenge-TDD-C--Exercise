using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
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

        public string RoverName { get; set; }

        public Point pointGridStart;
        public Point pointGridMax;
        public Point pointCurrent;

        public Rover(Plateau plateau)
        {
            //Initialising plateau and assigning value to it
            _plateau = new Plateau();
            _plateau = plateau;

            pointGridMax = new Point (_plateau.pointGridMax.X, _plateau.pointGridMax.Y);
            pointGridStart = new Point(_plateau.pointGridStart.X, _plateau.pointGridStart.Y);

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
            pointCurrent = new Point(xCoordinate, yCoordinate);
            CurrentDirectionFacing = _directionDict[direction];

            ValidateRoverPositionOnThePlateau(pointGridMax, pointCurrent);
        }

        /// <summary>
        /// Method to validate the Rover's position on the plateau
        /// </summary>
        /// <param name="gridMaxXCoordinate"></param>
        /// <param name="gridMaxYCoordinate"></param>
        /// <param name="xCoordinate"></param>
        /// <param name="yCoordinate"></param>
        /// <exception cref="ArgumentException"></exception>
        public void ValidateRoverPositionOnThePlateau(Point pointGridMax,Point pointCurrent)
        {
            var xValueOfRover = pointCurrent.X > 0 && pointCurrent.X < pointGridMax.X;
            var yValueOfRover = pointCurrent.Y > 0 && pointCurrent.Y < pointGridMax.Y;

            if (!(xValueOfRover && yValueOfRover))
                throw new ArgumentException("Rover position should not be outside the plateau grid.");
        }
    }
}