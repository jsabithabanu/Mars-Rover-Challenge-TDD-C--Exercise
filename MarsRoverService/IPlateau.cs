using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverService
{
    public interface IPlateau
    {
        public int GridMaxXCoordinate { get; set; }
        public int GridMaxYCoordinate { get; set; }
        public int GridStartXCoordinate { get; set; }
        public int GridStartYCoordinate { get; set; }

        public void SetPlateauGridSize(int gridMaxXCoordinate, int gridMaxYCoordinate);
    }
}