using CrossyRoad2D.Client.NetworkSync;
using CrossyRoad2D.Common.Enums;
using CrossyRoad2D.Common.MessageContents;

namespace CrossyRoad2D.Client.Commands.Chat
{
    public class MessageChatCommand : ICommand
    {
        private string _message;

        public MessageChatCommand(string message) 
        {
            _message = message;
        }

        public bool Execute()
        {
            if (!string.IsNullOrEmpty(_message))
            {
                NetworkState.Instance.SendMessage(NetworkMessageKind.ChatMessageToAll, new ChatMessageToAllMessageContent()
                {
                    Content = _message
                });
            }

            return true;
        }
    }
}
