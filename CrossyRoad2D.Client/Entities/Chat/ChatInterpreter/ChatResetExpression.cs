using CrossyRoad2D.Client.Commands.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.Chat.ChatInterpreter
{
    public class ChatResetExpression : IChatExpression
    {
        public void Interpret(ChatInterpreterContext context)
        {
            if (Regex.IsMatch(context.Message, @"^/reset(?:\s|$)"))
            {
                context.CommandsToExecute.Add(new ResetChatCommand());
            }
        }
    }
}
