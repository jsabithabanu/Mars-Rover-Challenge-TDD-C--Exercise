using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverService
{
    public interface IRover
    {
        public int CurrentXCoordinate { get; set; }
        public int CurrentYCoordinate { get; set; }
        public string CurrentDirectionFacing { get; set; }

        public void SetRoverPosition();
    }
}