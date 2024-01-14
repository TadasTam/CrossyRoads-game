using CrossyRoad2D.Common.MessageContents;

namespace CrossyRoad2D.Server.Services.Hub.Interfaces
{
    public interface IChatService
    {
        void MessageToAll(string connectionId, ChatMessageToAllMessageContent chatMessageToAllMessage);
        void MessageToConnection(string connectionId, ChatMessageToConnectionMessageContent chatMessageToConnectionMessage);
    }
}