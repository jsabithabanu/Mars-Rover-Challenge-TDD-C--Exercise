using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverService
{
    public class CommandCenter
    {
        public Direction CurrentDirectionFacing { get; set; }

        private Plateau _plateau;

        public CommandCenter()
        {
            _plateau = new Plateau();
        }

        /// <summary>
        /// Moves the Rover on the plateau based on the instructions given
        /// </summary>
        /// <param name="rover"></param>
        /// <param name="movementInstructions"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public string MoveRover(Rover rover, string movementInstructions)
        {
            var moveCommand = movementInstructions.ToCharArray();
            var instructions = moveCommand.Select(action => rover._instructionDict[action]).ToList();
            this.CurrentDirectionFacing = rover.CurrentDirectionFacing;

            foreach (var movement in instructions)
            {
                switch (movement)
                {
                    case RoverCommand.TurnLeft:
                        TurnLeft(CurrentDirectionFacing);
                        break;
                    case RoverCommand.TurnRight:
                        TurnRight(CurrentDirectionFacing);
                        break;
                    case RoverCommand.MoveForward:
                        MoveForward(rover);
                        break;
                    default:
                        //throw new ArgumentOutOfRangeException();
                        throw new ArgumentException("Invalid movement command to the Rover.");

                }
            }
            var directionKey = rover._directionDict.FirstOrDefault(x => x.Value.Equals(rover.CurrentDirectionFacing)).Key.ToString();
            var output = rover.CurrentXCoordinate + " " + rover.CurrentYCoordinate + " " + directionKey;
            return output;
        }

        /// <summary>
        /// Method to turn the Rover left - 90 degrees
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private Direction TurnLeft(Direction direction) =>
            direction switch
            {
                Direction.North => CurrentDirectionFacing = Direction.West,
                Direction.South => CurrentDirectionFacing = Direction.East,
                Direction.West => CurrentDirectionFacing = Direction.South,
                Direction.East => CurrentDirectionFacing = Direction.North,
                //_ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
                _ => throw new ArgumentException("Invalid direction command to the Rover.")
            };

        /// <summary>
        /// Method to turn the Rover right - 90 degrees
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private Direction TurnRight(Direction direction) =>
            direction switch
            {
                Direction.North => CurrentDirectionFacing = Direction.East,
                Direction.South => CurrentDirectionFacing = Direction.West,
                Direction.West => CurrentDirectionFacing = Direction.North,
                Direction.East => CurrentDirectionFacing = Direction.South,
                _ => throw new ArgumentException("Invalid direction command to the Rover.")
            };

        /// <summary>
        /// Method to move the Rover forward by one grid point, maintaining the same direction
        /// </summary>
        /// <param name="rover"></param>
        /// <exception cref="ArgumentException"></exception>
        private void MoveForward(Rover rover)
        {
            switch (CurrentDirectionFacing)
            {
                case Direction.North:
                    {
                        rover.CurrentYCoordinate += 1;
                        if (rover.CurrentYCoordinate > rover.GridMaxYCoordinate)
                            throw new ArgumentException("Rover cannot move outside the plateau. " +
                                "Please modify the instructions.");
                        break;
                    }
                case Direction.South:
                    {
                        rover.CurrentYCoordinate -= 1;
                        if (rover.CurrentYCoordinate < rover.GridStartYCoordinate)
                            throw new ArgumentException("Rover cannot move outside the plateau. " +
                                "Please modify the instructions.");
                        break;
                    }
                case Direction.East:
                    {
                        rover.CurrentXCoordinate += 1;
                        if (rover.CurrentXCoordinate > rover.GridMaxXCoordinate)
                            throw new ArgumentException("Rover cannot move outside the plateau. " +
                                "Please modify the instructions.");
                        break;
                    }
                case Direction.West:
                    {
                        rover.CurrentXCoordinate -= 1;
                        if (rover.CurrentXCoordinate < rover.GridStartXCoordinate)
                            throw new ArgumentException("Rover cannot move outside the plateau. " +
                                "Please modify the instructions.");
                        break;
                    }
            }
        }
    }
}