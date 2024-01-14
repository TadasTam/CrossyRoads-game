using CrossyRoad2D.Common.Enums;
using CrossyRoad2D.Common.MessageContents;
using CrossyRoad2D.Hubs;
using CrossyRoad2D.Server.Services.Hub.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace CrossyRoad2D.Server.Services.Hub
{
    public class ChatService : IChatService
    {
        private IHubClients _clients;

        public ChatService(IHubContext<GameHub> context)
        {
            _clients = context.Clients;
        }

        public void MessageToAll(string connectionId, ChatMessageToAllMessageContent chatMessageToAllMessage)
        {
            chatMessageToAllMessage.Content = $"{connectionId}: {chatMessageToAllMessage.Content}";
            _clients.All.SendAsync(Enum.GetName(NetworkMessageKind.ChatMessageToAll), chatMessageToAllMessage);
        }

        public void MessageToConnection(string connectionId, ChatMessageToConnectionMessageContent chatMessageToConnectionMessage)
        {
            _clients.Client(connectionId).SendAsync(Enum.GetName(NetworkMessageKind.ChatMessageToConnection), chatMessageToConnectionMessage);
        }
    }
}
