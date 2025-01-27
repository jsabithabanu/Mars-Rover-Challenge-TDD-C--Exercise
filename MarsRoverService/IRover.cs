﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverService
{
    public interface IRover
    {
        public int CurrentXCoordinate { get; set; }
        public int CurrentYCoordinate { get; set; }
        public Direction CurrentDirectionFacing { get; set; }

        public void SetRoverPosition(int xCoordinate, int yCoordinate, char direction);
    }
}