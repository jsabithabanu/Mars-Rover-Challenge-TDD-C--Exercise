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
       
        public void SetPlateauGridSize(int gridMaxXCoordinate, int gridMaxYCoordinate)
        {
            GridStartXCoordinate = _STARTX;
            GridStartYCoordinate = _STARTY;

            GridMaxXCoordinate = gridMaxXCoordinate;
            GridMaxYCoordinate = gridMaxYCoordinate;

            ValidatePlateauGridSize(gridMaxXCoordinate, gridMaxYCoordinate);            
        }

        public void ValidatePlateauGridSize(int gridMaxXCoordinate, int gridMaxYCoordinate)
        {
            if (gridMaxXCoordinate < 0 || GridMaxYCoordinate < 0)
                throw new ArgumentException("Please enter a valid plateau grid size.");

            if (gridMaxXCoordinate == 0 && GridMaxYCoordinate == 0)
                throw new ArgumentException("The plateau grid size must be greater than (0, 0)");
        }
    }
}