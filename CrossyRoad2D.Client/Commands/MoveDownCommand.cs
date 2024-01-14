using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Commands
{
    public class MoveDownCommand : MoveCommand
    {
        public MoveDownCommand() : base(offsetX: 0, offsetY: -1)
        {
        }
    }
}
