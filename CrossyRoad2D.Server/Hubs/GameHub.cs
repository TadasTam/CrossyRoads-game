using CrossyRoad2D.Common.Enums;
using CrossyRoad2D.Common.MessageContents;
using CrossyRoad2D.Server.Services;
using CrossyRoad2D.Server.Services.Hub.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace CrossyRoad2D.Hubs
{
    public class GameHub : Hub
    {
        private IPlayerService _playerService;
        private IChatService _chatService;
        private IConnectionService _connectionService;

        public GameHub(
            IPlayerService playerService,
            IChatService chatService,
            IConnectionService connectionService)
        {
            _playerService = playerService;
            _chatService = chatService;
            _connectionService = connectionService;
        }

        public override Task OnConnectedAsync()
        {
            _connectionService.AddNewConnection(Context.ConnectionId);
            _playerService.AddNewPlayer(Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _connectionService.RemoveConnection(Context.ConnectionId);
            _playerService.RemovePlayer(Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public void Death(object _)
        {
            _playerService.KillPlayer(Context.ConnectionId);
        }

        public void PlayerCopyDeath(PlayerCopyDeathMessageContent message)
        {
            _playerService.PlayerCopyDeath(Context.ConnectionId, message);
        }

        public void PlayerUpdate(PlayerUpdateMessageContent updateMessage)
        {
            _playerService.UpdatePlayer(Context.ConnectionId, updateMessage);
        }

        public void PlayerClone(PlayerCloneMessageContent cloneMessage)
        {
            _playerService.ClonePlayer(Context.ConnectionId, cloneMessage);
        }

        public void ChatMessageToAll(ChatMessageToAllMessageContent chatMessageToAllMessage)
        {
            _chatService.MessageToAll(Context.ConnectionId, chatMessageToAllMessage);
        }

        public void Login(LoginMessageContent loginMessage)
        {
            _connectionService.GiveAdmin(Context.ConnectionId, loginMessage);
        }

        public void Reset(object _)
        {
            _playerService.Reset(Context.ConnectionId);
        }

        public void Speed(object _)
        {
            _playerService.Speed(Context.ConnectionId);
        }
    }
}
