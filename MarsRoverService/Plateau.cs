using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MarsRoverService
{
    public class Plateau : IPlateau
    {

        private const int _STARTX = 0;
        private const int _STARTY = 0;
        public int GridStartXCoordinate { get; set; }
        public int GridStartYCoordinate { get; set; }
        public int GridMaxXCoordinate { get; set; }
        public int GridMaxYCoordinate { get; set; }
        //public bool GotObstacles { get; set; }

        public Point pointGridStart;
        public Point pointGridMax;



        public void SetPlateauGridSize(int gridMaxXCoordinate, int gridMaxYCoordinate)
        {
            pointGridStart = new Point(_STARTX, _STARTY);
            pointGridMax = new Point(gridMaxXCoordinate, gridMaxYCoordinate);
 
            ValidatePlateauGridSize(pointGridMax);
        }

        public void ValidatePlateauGridSize(Point pointGridSize)
        {
            if(pointGridSize.X < 0 || pointGridSize.Y < 0)
                throw new ArgumentException("Please enter a valid plateau grid size.");

            if(pointGridSize.X == _STARTX && pointGridSize.Y == _STARTY)
                throw new ArgumentException($"The plateau grid size must be greater than ({_STARTX}, {_STARTX})");
        }
    }
}