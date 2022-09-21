using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverService
{
    public class Plateau : IPlateau
    {
        public int GridEndXCoordinate { get; set; }
        public int GridEndYCoordinate { get; set; }
        public int GridStartXCoordinate { get; set; }
        public int GridStartYCoordinate { get; set; }

        public void SetPlateauGridSize()
        { 

        }
    }
}