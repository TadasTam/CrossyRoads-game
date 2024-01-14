using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Common.MessageContents
{
    public class PlayerUpdateMessageContent
    {
        public ServerPlayer ServerPlayer { get; set; }
    }
}
