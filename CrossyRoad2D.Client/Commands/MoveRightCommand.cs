using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Commands
{
    public class MoveRightCommand : MoveCommand
    {
        public MoveRightCommand() : base(offsetX : 1, offsetY : 0)
        {
        }
    }
}
