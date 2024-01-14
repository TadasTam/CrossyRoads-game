using CrossyRoad2D.Client.Commands;
using CrossyRoad2D.Client.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CrossyRoad2D.Client.Entities.Chat;
using CrossyRoad2D.Client.Commands.Chat;

namespace CrossyRoad2D.Client.Singletons.Controllers
{
    public abstract class BaseController
    {
        protected BaseController(Key? leftKey, Key? rightKey, Key? upKey, Key? downKey, Key? undoKey, Key? screenSizeKey, Key? volumeDownKey, Key? volumeUpKey, Key? openChatKey, Key? submitChatKey, Key? hideChatKey)
        {
            LeftKey = leftKey;
            RightKey = rightKey;
            UpKey = upKey;
            DownKey = downKey;
            UndoKey = undoKey;
            ScreenSizeKey = screenSizeKey;
            VolumeDownKey = volumeDownKey;
            VolumeUpKey = volumeUpKey;
            OpenChatKey = openChatKey;
            SubmitChatKey = submitChatKey;
            HideChatKey = hideChatKey;
        }

        public Key? LeftKey { get; set; }
        public Key? RightKey { get; set; }
        public Key? UpKey { get; set; }
        public Key? DownKey { get; set; }
        public Key? UndoKey { get; set; }
        public Key? ScreenSizeKey { get; set; }
        public Key? VolumeDownKey { get; set; }
        public Key? VolumeUpKey { get; set; }
        public Key? OpenChatKey { get; set; }
        public Key? SubmitChatKey { get; set; }
        public Key? HideChatKey { get; set; }

        public void Update()
        {
            var chat = EntityCollection.Instance.GetEntitiesOfType(EntityType.Chat)
                        .ToList()
                        .FirstOrDefault() as ChatEntity;

            if (chat == null || chat.IsChatActive == false)
            {
                if (LeftKey.HasValue && KeyboardState.Instance.IsKeyPressed(LeftKey.Value))
                {
                    CommandInvoker.Instance.Run(new MoveLeftCommand());
                }
                else if (RightKey.HasValue && KeyboardState.Instance.IsKeyPressed(RightKey.Value))
                {
                    CommandInvoker.Instance.Run(new MoveRightCommand());
                }
                else if (UpKey.HasValue && KeyboardState.Instance.IsKeyPressed(UpKey.Value))
                {
                    CommandInvoker.Instance.Run(new MoveUpCommand());
                }
                else if (DownKey.HasValue && KeyboardState.Instance.IsKeyPressed(DownKey.Value))
                {
                    CommandInvoker.Instance.Run(new MoveDownCommand());
                }

                if (UndoKey.HasValue && KeyboardState.Instance.IsKeyJustPressed(UndoKey.Value))
                {
                    CommandInvoker.Instance.TryUndo();
                }
                if (ScreenSizeKey.HasValue && KeyboardState.Instance.IsKeyJustPressed(ScreenSizeKey.Value))
                {
                    CommandInvoker.Instance.Run(new ChangeScreenSizeCommand());
                }
                if (VolumeDownKey.HasValue && KeyboardState.Instance.IsKeyJustPressed(VolumeDownKey.Value))
                {
                    CommandInvoker.Instance.Run(new VolumeDownCommand());
                }
                if (VolumeUpKey.HasValue && KeyboardState.Instance.IsKeyJustPressed(VolumeUpKey.Value))
                {
                    CommandInvoker.Instance.Run(new VolumeUpCommand());
                }
                if (OpenChatKey.HasValue && KeyboardState.Instance.IsKeyJustPressed(OpenChatKey.Value))
                {
                    CommandInvoker.Instance.Run(new OpenChatCommand());
                }
            }
            else
            {
                if (SubmitChatKey.HasValue && KeyboardState.Instance.IsKeyJustPressed(SubmitChatKey.Value))
                {
                    CommandInvoker.Instance.Run(new SubmitChatCommand());
                }
                else if (HideChatKey.HasValue && KeyboardState.Instance.IsKeyJustPressed(HideChatKey.Value))
                {
                    CommandInvoker.Instance.Run(new HideChatCommand());
                }
            }
        }
    }
}
