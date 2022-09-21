using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverService
{
    public interface ICommandCenter
    {
        public string Command { get; set; }

        public void MoveRoverByCommand();

        public void GetCommands();
    }
}