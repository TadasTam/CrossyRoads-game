using CrossyRoad2D.Client.NetworkSync;
using CrossyRoad2D.Common.Enums;
using CrossyRoad2D.Common.MessageContents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Commands.Chat
{
    public class ResetChatCommand : ICommand
    {
        public bool Execute()
        {
            NetworkState.Instance.SendMessage(NetworkMessageKind.Reset, new object { });
            return true;
        }
    }
}
