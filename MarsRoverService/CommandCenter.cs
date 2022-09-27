using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MarsRoverService
{
    public class CommandCenter
    {
        #region Variable declrations
        public Direction CurrentDirectionFacing { get; set; }

        private Plateau _plateau;
        private Rover _rover;
        public List<Rover> RoversList = new();
        public int PossibleNoOfRovers;

        #endregion


        public CommandCenter()
        {
            _plateau = new();
            RoversList = new();
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
            //Breaking down the movement instructions
            var moveCommand = movementInstructions.ToCharArray();
            var instructions = moveCommand.Select(action => rover._instructionDict[Char.ToUpper(action)]).ToList();
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

            //Setting the collision point after a move is done
            rover.collisionPoint = new Point(rover.pointCurrent.X, rover.pointCurrent.Y);

            //Setting up the output
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
            switch (CurrentDirectionFacing)
            {
                case Direction.North:
                    {
                        rover.CurrentDirectionFacing = Direction.North;
                        if (rover.pointCurrent.Y + 1 > rover.pointGridMax.Y)
                        {
                           rover.CurrentDirectionFacing = Direction.North;
                           CallException(rover);
                        }
                        else
                        {
                            rover.pointCurrent.Y += 1;

                            //Checking for collision points                            
                            CheckForCollisionPoints(rover, CurrentDirectionFacing);
                        }
                        break;
                    }
                case Direction.South:
                    {
                        rover.CurrentDirectionFacing = Direction.South;
                        if (rover.pointCurrent.Y - 1 < rover.pointGridStart.Y)
                        {
                            rover.CurrentDirectionFacing = Direction.South;
                            CallException(rover);
                        }
                        else
                        {
                            rover.pointCurrent.Y -= 1;

                            //Checking for collision points                           
                            CheckForCollisionPoints(rover, CurrentDirectionFacing);
                        }
                        break;
                    }
                case Direction.East:
                    {
                        rover.CurrentDirectionFacing = Direction.East;
                        if (rover.pointCurrent.X + 1 > rover.pointGridMax.X)
                        {
                            rover.CurrentDirectionFacing = Direction.East;
                            CallException(rover);
                        }
                        else
                        {
                            rover.pointCurrent.X += 1;

                            //Checking for collision points                            
                            CheckForCollisionPoints(rover, CurrentDirectionFacing);
                        }
                        break;
                    }
                case Direction.West:
                    {
                        rover.CurrentDirectionFacing = Direction.West;
                        if (rover.pointCurrent.X - 1 < rover.pointGridStart.X)
                        {
                            rover.CurrentDirectionFacing = Direction.West;
                            CallException(rover);
                        }                       
                        else
                        {
                            rover.pointCurrent.X -= 1;

                            //Checking for collision points                            
                            CheckForCollisionPoints(rover, CurrentDirectionFacing);
                        }
                        break;
                    }
            }
        }

        /// <summary>
        /// Method to throw an exception when the Rover should move outside the plateau based on the instructions
        /// </summary>
        /// <param name="rover"></param>
        /// <exception cref="ArgumentException"></exception>
        public void CallException(Rover rover)
        {
            throw new ArgumentException($"Rover cannot move outside the plateau. It now stands at the position " +
                    $"({rover.pointCurrent.X}, {rover.pointCurrent.Y}) facing {rover.CurrentDirectionFacing}. " +
                    $"Please modify the instructions.");
        }

        /// <summary>
        /// Method to check for the collision points and set back the current coordinates for Rover if found
        /// </summary>
        /// <param name="rover"></param>
        /// <param name="direction"></param>
        /// <exception cref="ArgumentException"></exception>
        public void CheckForCollisionPoints(Rover rover, Direction direction)
        {
            //Checking for collision points
            if (RoversList.Count > 0)
            {
                for (int i = 0; i < (RoversList.Count) - 1; i++)
                {
                    if (rover.pointCurrent.X == RoversList[i].collisionPoint.X &&
                        rover.pointCurrent.Y == RoversList[i].collisionPoint.Y)
                    {
                        //Setting the coordinate back to the previous point if collision point is found ahead
                        switch (direction)
                        {
                            case Direction.North:
                                rover.pointCurrent.Y -= 1;
                                break;
                            case Direction.South:
                                rover.pointCurrent.Y += 1;
                                break;
                            case Direction.East:
                                rover.pointCurrent.X -= 1;
                                break;
                            case Direction.West:
                                rover.pointCurrent.X += 1;
                                break;
                        }
                        throw new ArgumentException($"Rover cannot move further. There is a collision ahead. " +
                            $"It now stands at the position ({rover.pointCurrent.X}, {rover.pointCurrent.Y}) " +
                            $"facing {rover.CurrentDirectionFacing}. Please modify the instructions.");
                    }
                }
            }
        }
        
        /// <summary>
        /// Method to get the list of Rovers added
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<Rover> GetRoversList()
        {
            return RoversList.AsReadOnly();
        }

        /// <summary>
        /// Method to add Rovers on the plateau by the Command center
        /// </summary>
        /// <param name="xCoordinate"></param>
        /// <param name="yCoordinate"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public Rover AddRover(int xCoordinate, int yCoordinate, char direction)
        {
            /* Calculating the number of rovers that can be placed on the Plateau.
               This formula allows atleast 1 grid point for each Rover to move around.
               Based on this, it calculates the number of Rovers can be placed on the plateau. */
            var gridXpoints = _plateau.pointGridMax.X + 1;
            var gridYpoints = _plateau.pointGridMax.Y + 1;
            var roversOnX = Math.Round(Convert.ToDouble(gridXpoints) / 2, MidpointRounding.AwayFromZero);
            var roversOnY = Math.Round(Convert.ToDouble(gridYpoints) / 2, MidpointRounding.AwayFromZero);
            PossibleNoOfRovers = (int)(roversOnX * roversOnY);

            if (RoversList.Count <= PossibleNoOfRovers)
            {
                _rover = new Rover(_plateau);
                _rover.SetRoverPosition(xCoordinate, yCoordinate, direction);
                if(RoversList.Count > 0)
                {
                    _rover.collisionPoint = new(xCoordinate, yCoordinate);
                }
                RoversList.Add(_rover);
            }
            return _rover;
        }

        /// <summary>
        /// Method to add plateau by the Command center
        /// </summary>
        /// <param name="gridMaxX"></param>
        /// <param name="gridMaxY"></param>
        /// <returns></returns>
        public Plateau AddPlateau(int gridMaxX, int gridMaxY)
        {            
            _plateau.SetPlateauGridSize(gridMaxX, gridMaxY);
            return _plateau;
        }       
    }
}