using CrossyRoad2D.Client.Commands.Chat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.Chat.ChatInterpreter
{
    public class ChatSpeedExpression : IChatExpression
    {
        public void Interpret(ChatInterpreterContext context)
        {
            if (Regex.IsMatch(context.Message, @"^/speed(?:\s|$)"))
            {
                context.CommandsToExecute.Add(new SpeedChatCommand());
            }
        }
    }
}
