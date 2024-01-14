using CrossyRoad2D.Client.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.Chat.ChatInterpreter
{
    public class ChatInterpreterContext
    {
        public List<ICommand> CommandsToExecute { get; set; } = new();
        public string Message { get; set; }
    }
}
