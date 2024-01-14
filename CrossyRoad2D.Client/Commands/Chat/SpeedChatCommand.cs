using CrossyRoad2D.Client.NetworkSync;
using CrossyRoad2D.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Commands.Chat
{
    public class SpeedChatCommand : ICommand
    {
        public bool Execute()
        {
            NetworkState.Instance.SendMessage(NetworkMessageKind.Speed, new object { });
            return true;
        }
    }
}
