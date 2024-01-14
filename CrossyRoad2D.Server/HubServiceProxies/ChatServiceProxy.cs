using CrossyRoad2D.Common.MessageContents;
using CrossyRoad2D.Server.Services;
using CrossyRoad2D.Server.Services.Hub.Interfaces;

namespace CrossyRoad2D.Server.HubServiceProxies
{
    public class ChatServiceProxy : IChatService
    {
        private IChatService _baseChatService;
        private ILogger<ChatServiceProxy> _logger;

        public ChatServiceProxy(
            IChatService chatService,
            ILogger<ChatServiceProxy> logger)
        {
            _baseChatService = chatService;
            _logger = logger;
        }

        public void MessageToAll(string connectionId, ChatMessageToAllMessageContent chatMessageToAllMessage)
        {
            _baseChatService.MessageToAll(connectionId, chatMessageToAllMessage);
            _logger.LogInformation($"Player {connectionId} has wrote: {chatMessageToAllMessage.Content}");
        }

        public void MessageToConnection(string connectionId, ChatMessageToConnectionMessageContent chatMessageToConnectionMessage)
        {
            _baseChatService.MessageToConnection(connectionId, chatMessageToConnectionMessage);
            _logger.LogInformation($"Message was sent to connection {connectionId}: {chatMessageToConnectionMessage.Content}");
        }
    }
}
