using CrossyRoad2D.Client.Components.Xaml;
using CrossyRoad2D.Client.Entities.Chat.ChatInterpreter;
using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Facade;
using CrossyRoad2D.Client.NetworkSync;
using CrossyRoad2D.Client.Singletons;
using CrossyRoad2D.Common.Enums;
using CrossyRoad2D.Common.MessageContents;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CrossyRoad2D.Client.Entities.Chat
{
    class ChatEntity : Entity
    {
        private ChatXamlComponent _chatXamlComponent;
        private List<string> _chat;

        public bool IsChatActive
        {
            get
            {
                return _chatXamlComponent.TextboxFocused;
            }
        }

        public ChatEntity() : base(EntityType.Chat)
        {
            _chatXamlComponent = new ChatXamlComponent();
            RenderPriority = EntityRenderPriority.UI_Chat;
            _chat = new();
        }

        public override void Render(Canvas canvas)
        {
            _chatXamlComponent.Rectangle.X = 0;
            _chatXamlComponent.Rectangle.Y = canvas.ActualHeight - _chatXamlComponent.Rectangle.Height;
            _chatXamlComponent.Render(canvas, false);
        }

        public override void PrioritizedUpdate()
        {
            NetworkState
                .Instance
                .GetMessagesOfKind<ChatMessageToAllMessageContent>(NetworkMessageKind.ChatMessageToAll)
                .ToList()
                .ForEach(chatMessageToAll =>
                {
                    AddChatMessage(chatMessageToAll.Content);
                });

            NetworkState
                .Instance
                .GetMessagesOfKind<ChatMessageToConnectionMessageContent>(NetworkMessageKind.ChatMessageToConnection)
                .ToList()
                .ForEach(chatMessageToConnection =>
                {
                    AddChatMessage(chatMessageToConnection.Content);
                });
        }

        public void Show()
        {
            _chatXamlComponent.TextboxFocused = true;
        }

        public void Hide()
        {
            _chatXamlComponent.TextboxFocused = false;
            _chatXamlComponent.ClearTextboxContent();
        }

        public void Submit()
        {
            var context = new ChatInterpreterContext()
            {
                Message = _chatXamlComponent.TextboxContent
            };

            var expressions = new List<IChatExpression>()
            {
                new ChatLoginExpression(),
                new ChatMessageExpression(),
                new ChatResetExpression(),
                new ChatSpeedExpression(),
            };

            expressions.ForEach(expression => expression.Interpret(context));
            context.CommandsToExecute.ForEach(command => command.Execute());

            if(context.CommandsToExecute.Count == 0)
            {
                AddChatMessage("Unknown command");
            }

            _chatXamlComponent.TextboxFocused = false;
            _chatXamlComponent.ClearTextboxContent();
        }

        private void AddChatMessage(string message)
        {
            _chat.Insert(0, message);
            for (int i = 0; i < ChatXamlComponent.TEXT_BLOCK_COUNT && i < _chat.Count; i++)
            {
                _chatXamlComponent.SetChatEntry(i, _chat[i]);
            }

            if (_chat.Count > ChatXamlComponent.TEXT_BLOCK_COUNT)
            {
                _chat.RemoveRange(ChatXamlComponent.TEXT_BLOCK_COUNT, _chat.Count - ChatXamlComponent.TEXT_BLOCK_COUNT);
            }
        }
    }
}
