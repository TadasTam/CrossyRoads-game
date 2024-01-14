using CrossyRoad2D.Client.Entities;
using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Common.Enums;
using CrossyRoad2D.Common.MessageContents;
using CrossyRoad2D.Server.Models;
using System.Linq;

namespace CrossyRoad2D.Client.NetworkSync
{
    public class NetworkSyncedEntityVisitor : INetworkSyncedEntityVisitor
    {
        public void VisitPlayer(PlayerEntity playerEntity)
        {
            if (playerEntity.IsOfCurrentClient && playerEntity.UpdatedServerPlayerThisFrame)
            {
                SyncExistingPlayerUpdate(playerEntity);
            }

            if (playerEntity.IsOfCurrentClient && playerEntity.SyncedToNetwork == false && playerEntity.GetPlayerEntityCopyId() > 0)
            {
                SyncPlayerCreation(playerEntity);
            }

            if (playerEntity.IsOfCurrentClient && playerEntity.WillBeRemovedNextFrame && playerEntity.SyncedToNetwork == true)
            {
                SyncPlayerRemoval(playerEntity);
            }
        }

        private void SyncExistingPlayerUpdate(PlayerEntity playerEntity)
        {
            NetworkState.Instance.SendMessage(NetworkMessageKind.PlayerUpdate,
                    new PlayerUpdateMessageContent()
                    {
                        ServerPlayer = playerEntity.ServerPlayer,
                    }
                );
            playerEntity.UpdatedServerPlayerThisFrame = false;
        }

        private void SyncPlayerCreation(PlayerEntity playerEntity)
        {
            NetworkState.Instance.SendMessage(NetworkMessageKind.PlayerClone,
                        new PlayerCloneMessageContent()
                        {
                            ServerPlayer = playerEntity.ServerPlayer,
                        }
                    );

            playerEntity.SyncedToNetwork = true;
        }

        private void SyncPlayerRemoval(PlayerEntity playerEntity)
        {
            var otherPlayerEntitiesLeft = EntityCollection.Instance
                    .GetEntitiesOfType(EntityType.Player)
                    .Any(entity =>
                    {
                        var iterationPlayer = entity as PlayerEntity;
                        return iterationPlayer.IsOfCurrentClient && iterationPlayer.Id != playerEntity.Id && !iterationPlayer.WillBeRemovedNextFrame;
                    });

            if (otherPlayerEntitiesLeft)
            {
                NetworkState.Instance.SendMessage(NetworkMessageKind.PlayerCopyDeath,
                    new PlayerCopyDeathMessageContent()
                    {
                        ServerPlayer = playerEntity.ServerPlayer
                    }
                );

                playerEntity.SyncedToNetwork = false;
            }
            else
            {
                NetworkState.Instance.SendMessage(NetworkMessageKind.Death, new object { });

                EntityCollection.Instance
                    .GetEntitiesOfType(EntityType.Player)
                    .ToList()
                    .ForEach(entity =>
                    {
                        var iterationPlayer = entity as PlayerEntity;
                        iterationPlayer.SyncedToNetwork = false;
                    });
            }
        }
    }
}
