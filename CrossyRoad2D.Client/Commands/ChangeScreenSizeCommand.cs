using CrossyRoad2D.Client.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Commands
{
    public class ChangeScreenSizeCommand : ICommand
    {
        public bool Execute()
        {
            Settings.getInstance().ChangeScreenSize();
            return true;
        }
    }
}
