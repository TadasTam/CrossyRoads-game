using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.Chat.ChatInterpreter
{
    public interface IChatExpression
    {
        void Interpret(ChatInterpreterContext context);
    }
}
