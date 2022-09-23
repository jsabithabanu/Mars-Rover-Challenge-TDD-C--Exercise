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
        private readonly Dictionary<char, Direction> _directionDict;
        private readonly Dictionary<char, RoverCommand> _instructionDict;

        public int CurrentXCoordinate { get; set; }
        public int CurrentYCoordinate { get; set; }
        public Direction CurrentDirectionFacing { get; set; }

        public Rover()
        {
            _plateau = new Plateau();
            /* CurrentXCoordinate = _DEFAULT_X_VALUE;
             CurrentYCoordinate = _DEFAULT_Y_VALUE;
             CurrentDirectionFacing = _DEFAULT_FACING;
            */
            _directionDict = new Dictionary<char, Direction>()
           {
               { 'N', Direction.North },
               { 'S', Direction.South },
               { 'W', Direction.West },
               { 'E', Direction.East }
           };

            _instructionDict = new Dictionary<char, RoverCommand>()
            {
                { 'L', RoverCommand.Left },
                { 'R', RoverCommand.Right },
                { 'M', RoverCommand.MoveForward }
            };
        }

        public void SetRoverPosition(int gridMaxXCoordinate, int gridMaxYCoordinate,
            int xCoordinate, int yCoordinate, char direction)
        {
            CurrentXCoordinate = xCoordinate;
            CurrentYCoordinate = yCoordinate;
            CurrentDirectionFacing = _directionDict[direction];

            ValidateRoverPositionOnThePlateau(gridMaxXCoordinate, gridMaxYCoordinate, xCoordinate, yCoordinate);

        }
        public void ValidateRoverPositionOnThePlateau(int gridMaxXCoordinate, int gridMaxYCoordinate,
            int xCoordinate, int yCoordinate)
        {
            var xValueOfRover = xCoordinate >= 0 && xCoordinate <= gridMaxXCoordinate;
            var yValueOfRover = yCoordinate >= 0 && yCoordinate <= gridMaxYCoordinate;

            if (!(xValueOfRover && yValueOfRover))
                throw new ArgumentException("Rover position should not be outside the plateau grid.");

        }

        public void MoveRover(int gridMaxXCoordinate, int gridMaxYCoordinate,
            int xCoordinate, int yCoordinate, char direction, string movementInstructions)
        {
            var moveCommand = movementInstructions.ToCharArray();
            var instructions = moveCommand.Select(action => _instructionDict[action]).ToList();
            CurrentDirectionFacing = _directionDict[direction];

            foreach (var movement in instructions)
            {
                switch (movement)
                {
                    case RoverCommand.Left:
                        LeftMove(CurrentDirectionFacing);
                        break;
                    case RoverCommand.Right:
                        RightMove(CurrentDirectionFacing);
                        break;
                    case RoverCommand.MoveForward:
                        MoveForward(CurrentDirectionFacing);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();

                }
            }
        }

        private Direction LeftMove(Direction direction) =>
            direction switch
            {
                Direction.North => CurrentDirectionFacing = Direction.West,
                Direction.South => CurrentDirectionFacing = Direction.East,
                Direction.West => CurrentDirectionFacing = Direction.South,
                Direction.East => CurrentDirectionFacing = Direction.North,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

        private Direction RightMove(Direction direction) =>
            direction switch
            {
                Direction.North => CurrentDirectionFacing = Direction.East,
                Direction.South => CurrentDirectionFacing = Direction.West,
                Direction.West => CurrentDirectionFacing = Direction.North,
                Direction.East => CurrentDirectionFacing = Direction.South,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

        private void MoveForward(Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    CurrentYCoordinate = CurrentYCoordinate + 1;
                    //CurrentDirectionFacing = Direction.North;
                    break;
                case Direction.South:
                    CurrentYCoordinate = CurrentYCoordinate - 1;
                    //CurrentDirectionFacing = Direction.South;
                    break;
                case Direction.East:
                    CurrentXCoordinate = CurrentXCoordinate + 1;
                    //CurrentDirectionFacing = Direction.East;
                    break;
                case Direction.West:
                    CurrentXCoordinate = CurrentXCoordinate - 1;
                    //CurrentDirectionFacing = Direction.West;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            };
        }
    
    }
}