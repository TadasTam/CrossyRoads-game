using CrossyRoad2D.Client.Commands.Chat;
using System.Linq;
using System.Text.RegularExpressions;

namespace CrossyRoad2D.Client.Entities.Chat.ChatInterpreter
{
    public class ChatLoginExpression : IChatExpression
    {
        public void Interpret(ChatInterpreterContext context)
        {
            if(Regex.IsMatch(context.Message, @"^/login(?:\s|$)"))
            {
                string password = context.Message.Split(' ').ElementAtOrDefault(1);
                context.CommandsToExecute.Add(new LoginChatCommand(password));
            }
        }
    }
}
