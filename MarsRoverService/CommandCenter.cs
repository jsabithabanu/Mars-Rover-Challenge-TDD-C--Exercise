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
        private Rover _rover;
        private Rover _rovers;
        public List<Rover> RoverList = new();

        public CommandCenter()
        {
            _plateau = new();
            RoverList = new();
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
                        throw new ArgumentException("Invalid movement command to the Rover.");

                }
            }
            var directionKey = rover._directionDict.FirstOrDefault(x => x.Value.Equals(rover.CurrentDirectionFacing)).Key.ToString();
            var output = rover.pointCurrent.X + " " + rover.pointCurrent.Y + " " + directionKey;
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
            string exceptionMessage = "Rover cannot move outside the plateau. Please modify the instructions.";          

            switch (CurrentDirectionFacing)
            {
                case Direction.North:
                    {
                        rover.pointCurrent.Y += 1;
                        if (rover.pointCurrent.Y > rover.pointGridMax.Y) 
                            throw new ArgumentException(exceptionMessage);
                        break;
                    }
                case Direction.South:
                    {
                        rover.pointCurrent.Y -= 1;
                        if (rover.pointCurrent.Y < rover.pointGridStart.Y)
                            throw new ArgumentException(exceptionMessage);
                        break;
                    }
                case Direction.East:
                    {
                        rover.pointCurrent.X += 1;
                        if (rover.pointCurrent.X > rover.pointGridMax.X)
                            throw new ArgumentException(exceptionMessage);
                        break;
                    }
                case Direction.West:
                    {
                        rover.pointCurrent.X -= 1;
                        if (rover.pointCurrent.X < rover.pointGridStart.X)
                            throw new ArgumentException(exceptionMessage);
                        break;
                    }
            }
        }
        public void AddRover(int xCoordinate, int yCoordinate, char direction)
        {
            if ((RoverList != null) && (!RoverList.Any()))
            {
                var gridXpoints = _plateau.pointGridMax.X + 1;
                var gridYpoints = _plateau.pointGridMax.Y + 1;
                var roversOnX = Math.Round(Convert.ToDouble(gridXpoints) / 2, MidpointRounding.AwayFromZero);
                var roversOnY = Math.Round(Convert.ToDouble(gridYpoints) / 2, MidpointRounding.AwayFromZero);
                var possibleNoOfRovers = roversOnX * roversOnY;

                if (RoverList.Count <= possibleNoOfRovers)
                {
                    _rover = new Rover(_plateau);
                    _rover.SetRoverPosition(xCoordinate, yCoordinate, direction);
                    RoverList.Add(_rover);
                }
            }
        }

        public Plateau AddPlateau(int gridMaxX, int gridMaxY)
        {            
            _plateau.SetPlateauGridSize(gridMaxX, gridMaxY);
            return _plateau;
        }

      
    }
}