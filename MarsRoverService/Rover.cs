using System;
using System.Collections;
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
                { 'L', RoverCommand.TurnLeft },
                { 'R', RoverCommand.TurnRight },
                { 'M', RoverCommand.MoveForward }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridMaxXCoordinate"></param>
        /// <param name="gridMaxYCoordinate"></param>
        /// <param name="xCoordinate"></param>
        /// <param name="yCoordinate"></param>
        /// <param name="direction"></param>
        public void SetRoverPosition(int gridMaxXCoordinate, int gridMaxYCoordinate,
            int xCoordinate, int yCoordinate, char direction)
        {
            CurrentXCoordinate = xCoordinate;
            CurrentYCoordinate = yCoordinate;
            CurrentDirectionFacing = _directionDict[direction];

            ValidateRoverPositionOnThePlateau(gridMaxXCoordinate, gridMaxYCoordinate, xCoordinate, yCoordinate);

        }

        /// <summary>
        /// 
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

        /// <summary>
        /// Moves the Rover on the plateau based on the instructions given
        /// </summary>
        /// <param name="gridMaxXCoordinate"></param>
        /// <param name="gridMaxYCoordinate"></param>
        /// <param name="xCoordinate"></param>
        /// <param name="yCoordinate"></param>
        /// <param name="direction"></param>
        /// <param name="movementInstructions"></param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        ///
        public string MoveRover(int gridMaxXCoordinate, int gridMaxYCoordinate,
            int xCoordinate, int yCoordinate, char direction, string movementInstructions)
        {
            var moveCommand = movementInstructions.ToCharArray();
            var instructions = moveCommand.Select(action => _instructionDict[action]).ToList();
            CurrentXCoordinate = xCoordinate;
            CurrentYCoordinate = yCoordinate;
            CurrentDirectionFacing = _directionDict[direction];

            foreach (var movement in instructions)
            {
                switch (movement)
                {
                    case RoverCommand.TurnLeft:
                        MoveLeft(CurrentDirectionFacing);
                        break;
                    case RoverCommand.TurnRight:
                        MoveRight(CurrentDirectionFacing);
                        break;
                    case RoverCommand.MoveForward:
                        MoveForward(gridMaxXCoordinate, gridMaxYCoordinate, CurrentDirectionFacing);
                        break;
                    default:
                        //throw new ArgumentOutOfRangeException();
                        throw new ArgumentException("Invalid movement command to the Rover.");

                }
            }
            var directionKey = _directionDict.FirstOrDefault(x => x.Value.Equals(CurrentDirectionFacing)).Key.ToString();
            var output = CurrentXCoordinate + " " + CurrentYCoordinate + " " + directionKey;
            return output;
        }

        private Direction MoveLeft(Direction direction) =>
            direction switch
            {
                Direction.North => CurrentDirectionFacing = Direction.West,
                Direction.South => CurrentDirectionFacing = Direction.East,
                Direction.West => CurrentDirectionFacing = Direction.South,
                Direction.East => CurrentDirectionFacing = Direction.North,
                //_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                _ => throw new ArgumentException("Invalid direction command to the Rover.")
            };


        private Direction MoveRight(Direction direction) =>
            direction switch
            {
                Direction.North => CurrentDirectionFacing = Direction.East,
                Direction.South => CurrentDirectionFacing = Direction.West,
                Direction.West => CurrentDirectionFacing = Direction.North,
                Direction.East => CurrentDirectionFacing = Direction.South,
                _ => throw new ArgumentException("Invalid direction command to the Rover.")
            };

        private void MoveForward(int gridMaxXCoordinate, int gridMaxYCoordinate, Direction direction)
        {
            switch (direction)
            {
                case Direction.North:
                    {
                        CurrentYCoordinate += 1;
                        if (CurrentYCoordinate > gridMaxYCoordinate)
                            throw new ArgumentException("Rover cannot move outside the plateau. " +
                                "Please modify the instructions.");
                        break;
                    }
                case Direction.South:
                    {
                        CurrentYCoordinate -= 1;
                        if (CurrentYCoordinate < 0)
                            throw new ArgumentException("Rover cannot move outside the plateau. " +
                                "Please modify the instructions.");
                        break;
                    }
                case Direction.East:
                    {
                        CurrentXCoordinate += 1;
                        if (CurrentXCoordinate > gridMaxXCoordinate)
                            throw new ArgumentException("Rover cannot move outside the plateau. " +
                                "Please modify the instructions.");
                        break;
                    }
                case Direction.West:
                    {
                        CurrentXCoordinate -= 1;
                        if (CurrentXCoordinate < 0)
                            throw new ArgumentException("Rover cannot move outside the plateau. " +
                                "Please modify the instructions.");
                        break;
                    }
            }
        }
    }
}