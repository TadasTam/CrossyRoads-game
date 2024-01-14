using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Common.Enums;
using System.Collections.Generic;
using System.Linq;
using CrossyRoad2D.Common.MessageContents;
using CrossyRoad2D.Client.Iterator;
using CrossyRoad2D.Client.Entities.ItemsFactory;
using CrossyRoad2D.Client.Entities;

namespace CrossyRoad2D.Client.NetworkSync
{
    public class PlayerSpawnerEntity : Entity
    {
        public PlayerSpawnerEntity() : base(EntityType.PlayerManager) { }

        public override void PrioritizedUpdate()
        {
            NetworkState
                .Instance
                .GetMessagesOfKind<object>(NetworkMessageKind.Reset)
                .ToList()
                .ForEach(_ =>
                {
                    EntityCollection.Instance
                        .GetEntitiesOfType(EntityType.Player)
                        .ToList()
                        .ForEach(EntityCollection.Instance.RemoveEntity);
                });

            NetworkState
                .Instance
                .GetMessagesOfKind<object>(NetworkMessageKind.Speed)
                .ToList()
                .ForEach(_ =>
                {
                    EntityCollection.Instance
                        .GetEntitiesOfType(EntityType.Player)
                        .Select(entity => entity as PlayerEntity)
                        .Where(player => player.IsOfCurrentClient)
                        .ToList()
                        .ForEach(player =>
                        {
                            var creator = new ItemCreator();
                            var apple = creator.factorySpawnItem("Apple", new Position(-1, -1));
                            apple.Consume(player);
                        });
                });

            NetworkState
                .Instance
                .GetMessagesOfKind<PlayerCopyDeathMessageContent>(NetworkMessageKind.PlayerCopyDeath)
                .ToList()
                .ForEach(playerCopyDeathRequest =>
                {
                    List<Entity> toRemove = new List<Entity>();
                    IIterator iterator = EntityCollection.Instance.createIterator(EntityType.Player);
                    while (iterator.hasMore())
                    {
                        PlayerEntity player = (PlayerEntity)iterator.getNext();

                        if (player.GetPlayerServerId() == playerCopyDeathRequest.ServerPlayer.PlayerServerId && player.GetPlayerEntityCopyId() == playerCopyDeathRequest.ServerPlayer.PlayerCopyId)
                        {
                            toRemove.Add(player);
                        }
                    }
                    toRemove.ForEach(EntityCollection.Instance.RemoveEntity);
                });

            NetworkState
                .Instance
                .GetMessagesOfKind<PlayerUpdateMessageContent>(NetworkMessageKind.PlayerUpdate)
                .ToList()
                .ForEach(playerUpdateRequest =>
                {
                    IIterator iterator = EntityCollection.Instance.createIterator(EntityType.Player);
                    while (iterator.hasMore())
                    {
                        PlayerEntity player = (PlayerEntity)iterator.getNext();

                        if (player.GetPlayerServerId() == playerUpdateRequest.ServerPlayer.PlayerServerId && player.GetPlayerEntityCopyId() == playerUpdateRequest.ServerPlayer.PlayerCopyId)
                        {
                            player.SetPosition(playerUpdateRequest.ServerPlayer.Position);
                            EntityCollection.Instance.UpdateEntity(player);
                        }
                    }
                });

            NetworkState
                .Instance
                .GetMessagesOfKind<PlayerCloneMessageContent>(NetworkMessageKind.PlayerClone)
                .ToList()
                .ForEach(playerCloneRequest =>
                {
                    EntityCollection.Instance.AddEntity(new PlayerEntity(isOfCurrentClient: false, playerCloneRequest.ServerPlayer, syncedToNetwork: true));
                });

            NetworkState
                .Instance
                .GetMessagesOfKind<PlayerDisconnectedMessageContent>(NetworkMessageKind.PlayerDisconnected)
                .ToList()
                .ForEach(playerDisconnectedRequest =>
                {
                    List<Entity> toRemove = new List<Entity>();
                    IIterator iterator = EntityCollection.Instance.createIterator(EntityType.Player);
                    while (iterator.hasMore())
                    {
                        PlayerEntity player = (PlayerEntity)iterator.getNext();

                        if (player.GetPlayerServerId() == playerDisconnectedRequest.PlayerServerId)
                        {
                            toRemove.Add(player);
                        }
                    }
                    toRemove.ForEach(EntityCollection.Instance.RemoveEntity);
                });

            NetworkState
                .Instance
                .GetMessagesOfKind<PlayerConnectionSuccessfulMessageContent>(NetworkMessageKind.PlayerConnectionSuccessful)
                .ToList()
                .ForEach(playerConnectionSuccessfulRequest =>
                {
                    EntityCollection.Instance.AddEntity(new PlayerEntity(isOfCurrentClient: true, playerConnectionSuccessfulRequest.JustConnectedPlayer, syncedToNetwork: true));

                    playerConnectionSuccessfulRequest.OtherPlayers.ForEach(otherPlayer =>
                    {
                        EntityCollection.Instance.AddEntity(new PlayerEntity(isOfCurrentClient: false, otherPlayer, syncedToNetwork: true));
                    });
                });

            NetworkState
                .Instance
                .GetMessagesOfKind<PlayerConnectionSuccessfulMessageContent>(NetworkMessageKind.PlayerRespawnSuccessful)
                .ToList()
                .ForEach(playerConnectionSuccessfulRequest =>
                {
                    EntityCollection.Instance.AddEntity(new PlayerEntity(isOfCurrentClient: true, playerConnectionSuccessfulRequest.JustConnectedPlayer, syncedToNetwork: true));
                });

            NetworkState
                .Instance
                .GetMessagesOfKind<PlayerSpawnMessageContent>(NetworkMessageKind.PlayerSpawn)
                .ToList()
                .ForEach(playerSpawnRequest =>
                {
                    EntityCollection.Instance.AddEntity(new PlayerEntity(isOfCurrentClient: false, playerSpawnRequest.ServerPlayer, syncedToNetwork: true));
                });
        }
    }
}
