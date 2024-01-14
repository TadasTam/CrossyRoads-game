using CrossyRoad2D.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.NetworkSync
{
    public interface INetworkSyncedEntityVisitor
    {
        void VisitPlayer(PlayerEntity playerEntity);
    }
}
