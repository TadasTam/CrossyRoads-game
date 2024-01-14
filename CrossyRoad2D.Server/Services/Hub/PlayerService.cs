using CrossyRoad2D.Common.Enums;
using CrossyRoad2D.Common.MessageContents;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Hubs;
using CrossyRoad2D.Server.Models;
using CrossyRoad2D.Server.Services.Hub.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;

namespace CrossyRoad2D.Server.Services.Hub
{
    public class PlayerService : IPlayerService
    {
        private List<ServerPlayer> _players = new();
        private IHubClients _clients;
        private IConnectionService _connectionService;

        public PlayerService(IHubContext<GameHub> context, IConnectionService connectionService)
        {
            _clients = context.Clients;
            _connectionService = connectionService;
        }

        public void AddNewPlayer(string connectionId)
        {
            var newPlayer = GenerateNewPlayer(connectionId);
            _players.Add(newPlayer);

            _clients.Client(connectionId).SendAsync(
                Enum.GetName(NetworkMessageKind.PlayerConnectionSuccessful),
                new PlayerConnectionSuccessfulMessageContent()
                {
                    JustConnectedPlayer = newPlayer,
                    OtherPlayers = _players.Where(player => player.PlayerServerId != connectionId).ToList()
                });
            _clients.AllExcept(connectionId).SendAsync(
                Enum.GetName(NetworkMessageKind.PlayerSpawn),
                new PlayerSpawnMessageContent()
                {
                    ServerPlayer = newPlayer
                });
        }

        public void UpdatePlayer(string connectionId, PlayerUpdateMessageContent updateMessage)
        {
            _players = _players
                .Select(playerToUpdate =>
                {
                    if (playerToUpdate.PlayerServerId == updateMessage.ServerPlayer.PlayerServerId && playerToUpdate.PlayerCopyId == updateMessage.ServerPlayer.PlayerCopyId)
                    {
                        playerToUpdate.Position = updateMessage.ServerPlayer.Position;
                        return playerToUpdate;
                    }
                    else
                    {
                        return playerToUpdate;
                    }
                })
                .ToList();

            _clients.AllExcept(connectionId).SendAsync(
                Enum.GetName(NetworkMessageKind.PlayerUpdate),
                updateMessage);
        }

        public void ClonePlayer(string connectionId, PlayerCloneMessageContent cloneMessage)
        {
            _players.Add(cloneMessage.ServerPlayer);

            _clients.AllExcept(connectionId).SendAsync(
                Enum.GetName(NetworkMessageKind.PlayerClone),
                cloneMessage);
        }
        public void PlayerCopyDeath(string connectionId, PlayerCopyDeathMessageContent playerCopyDeathMessage)
        {
            _clients.AllExcept(connectionId).SendAsync(
                Enum.GetName(NetworkMessageKind.PlayerCopyDeath),
                playerCopyDeathMessage);
        }

        public void RemovePlayer(string connectionId)
        {
            _players = _players.Where(player => player.PlayerServerId != connectionId).ToList();

            _clients.AllExcept(connectionId).SendAsync(
                Enum.GetName(NetworkMessageKind.PlayerDisconnected),
                new PlayerDisconnectedMessageContent()
                {
                    PlayerServerId = connectionId
                });
        }

        public void KillPlayer(string connectionId)
        {
            RemovePlayer(connectionId);
            AddNewPlayer(connectionId);
        }

        public void Reset(string connectionId)
        {
            _players = new();
            _clients.All.SendAsync(Enum.GetName(NetworkMessageKind.Reset), new object { });
            _connectionService.GetAllConnectionIds().ForEach(AddNewPlayer);
        }

        public void Speed(string connectionId)
        {
            _clients.Client(connectionId).SendAsync(Enum.GetName(NetworkMessageKind.Speed), new object { });
        }

        private ServerPlayer GenerateNewPlayer(string connectionId)
        {
            return new ServerPlayer()
            {
                Color = Color.GetRandomStartingPlayerColor(),
                Position = Position.GetRandomPlayerStartingPosition(),
                PlayerServerId = connectionId,
                PlayerCopyId = 0,
                IsBadCopy = false
            };
        }
    }
}
