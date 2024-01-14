using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Common.Enums
{
	public enum NetworkMessageKind
	{
		PlayerConnectionSuccessful,
		PlayerSpawn,
		PlayerUpdate,
		PlayerDisconnected,
		PlayerClone,
		Death,
		PlayerCopyDeath,
        PlayerRespawnSuccessful,
		ChatMessageToAll,
		ChatMessageToConnection,
		Login,
		Reset,
		Speed
    }
}
