using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarsRoverService
{
    public class CommandCenter : ICommandCenter
    {
        public string Command { get; set; }

        public void MoveRoverByCommand()
        {
            throw new System.NotImplementedException();
        }

        public void GetCommands()
        {
            throw new System.NotImplementedException();
        }
    }
}