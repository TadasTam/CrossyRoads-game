using CrossyRoad2D.Client.Entities.Chat;
using CrossyRoad2D.Client.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Commands.Chat
{
    public class HideChatCommand : ICommand
    {
        public bool Execute()
        {
            EntityCollection.Instance.GetEntitiesOfType(EntityType.Chat)
                        .ToList()
                        .ForEach(entity =>
                        {
                            var chat = (ChatEntity)entity;
                            chat.Hide();
                        });

            return true;
        }
    }
}
