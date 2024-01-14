using CrossyRoad2D.Client.Commands;
using CrossyRoad2D.Client.Commands.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.Chat.ChatInterpreter
{
    public class ChatMessageExpression : IChatExpression
    {
        public void Interpret(ChatInterpreterContext context)
        {
            if(!context.Message.StartsWith("/"))
            {
                context.CommandsToExecute.Add(new MessageChatCommand(context.Message));
            }
        }
    }
}
