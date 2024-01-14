using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.NetworkSync
{
    public interface INetworkSyncedEntity
    {
        void Accept(INetworkSyncedEntityVisitor visitor);
    }
}
