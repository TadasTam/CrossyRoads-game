using CrossyRoad2D.Client.NetworkSync;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Commands
{
    public class DisconnectCommand : ICommand
    {
        public bool Execute()
        {
            NetworkState.Instance.Disconnect();
            return true;
        }
    }
}
