using CrossyRoad2D.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CrossyRoad2D.Common.Models
{
    public class NetworkMessage
    {
        public NetworkMessageKind Kind { get; init; }
        public JsonElement Content { get; init; }
    }
}
