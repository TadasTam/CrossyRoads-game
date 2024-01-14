using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Commands
{
    public class MoveUpCommand : MoveCommand
    {
        public MoveUpCommand() : base(offsetX: 0, offsetY: 1)
        {
        }
    }
}
