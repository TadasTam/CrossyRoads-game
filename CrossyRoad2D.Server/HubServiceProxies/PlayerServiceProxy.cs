using CrossyRoad2D.Common.MessageContents;
using CrossyRoad2D.Server.Services;
using CrossyRoad2D.Server.Services.Hub.Interfaces;
using Microsoft.AspNetCore.SignalR.Protocol;
using System.Security.Cryptography.X509Certificates;

namespace CrossyRoad2D.Server.HubServiceProxies
{
    public class PlayerServiceProxy : IPlayerService
    {
        private IPlayerService _basePlayerService;
        private ILogger<PlayerServiceProxy> _logger;
        private IConnectionService _connectionService;
        private IChatService _chatService;

        public PlayerServiceProxy(
            IPlayerService playerService,
            ILogger<PlayerServiceProxy> logger,
            IConnectionService connectionService,
            IChatService chatService)
        {
            _basePlayerService = playerService;
            _logger = logger;
            _connectionService = connectionService;
            _chatService = chatService;
        }

        public void AddNewPlayer(string connectionId)
        {
            _basePlayerService.AddNewPlayer(connectionId);
            _logger.LogInformation($"Added new player for connection {connectionId}");
        }

        public void ClonePlayer(string connectionId, PlayerCloneMessageContent cloneMessage)
        {
            if (cloneMessage.ServerPlayer.PlayerServerId != connectionId)
            {
                return;
            }

            _basePlayerService.ClonePlayer(connectionId, cloneMessage);
            _logger.LogTrace($"Cloned player for connection {connectionId}");
        }

        public void KillPlayer(string connectionId)
        {
            _basePlayerService.KillPlayer(connectionId);
            _logger.LogInformation($"Killed player for connection {connectionId}");
        }

        public void PlayerCopyDeath(string connectionId, PlayerCopyDeathMessageContent playerCopyDeathMessage)
        {
            _basePlayerService.PlayerCopyDeath(connectionId, playerCopyDeathMessage);
            _logger.LogTrace($"Copy killed player for connection {connectionId}");
        }

        public void RemovePlayer(string connectionId)
        {
            _basePlayerService.RemovePlayer(connectionId);
            _logger.LogInformation($"Removed player for connection {connectionId}");
        }

        public void UpdatePlayer(string connectionId, PlayerUpdateMessageContent updateMessage)
        {
            if (updateMessage.ServerPlayer.PlayerServerId != connectionId)
            {
                return;
            }

            _basePlayerService.UpdatePlayer(connectionId, updateMessage);
            _logger.LogTrace($"Updated player for connection {connectionId}");
        }

        public void Reset(string connectionId)
        {
            if (!_connectionService.IsConnectionAdmin(connectionId))
            {
                _chatService.MessageToConnection(connectionId,
                    new ChatMessageToConnectionMessageContent()
                    {
                        Content = "/reset: You need to be an admin to use this command"
                    });

                _logger.LogInformation($"{connectionId} has tried to use reset command");
                return;
            }

            _basePlayerService.Reset(connectionId);

            _chatService.MessageToAll(connectionId, new ChatMessageToAllMessageContent()
                    {
                        Content = "Game has been reset"
                    });
            _logger.LogInformation($"{connectionId} has reset the game");
        }

        public void Speed(string connectionId)
        {
            if (!_connectionService.IsConnectionAdmin(connectionId))
            {
                _chatService.MessageToConnection(connectionId,
                    new ChatMessageToConnectionMessageContent()
                    {
                        Content = "/speed: You need to be an admin to use this command"
                    });

                _logger.LogInformation($"{connectionId} has tried to use speed command");
                return;
            }

            _basePlayerService.Speed(connectionId);

            _chatService.MessageToConnection(connectionId, new ChatMessageToConnectionMessageContent()
            {
                Content = "You have used the speed command"
            });
            _logger.LogInformation($"{connectionId} has used the speed command");
        }
    }
}
