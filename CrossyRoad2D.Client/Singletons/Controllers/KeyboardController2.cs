using CrossyRoad2D.Client.Commands;
using CrossyRoad2D.Client.Singletons.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CrossyRoad2D.Client.Singletons
{
    public class KeyboardController2 : BaseController
    {
        private static KeyboardController2 _instance;
        public static KeyboardController2 Instance
        {
            get
            {
                _instance ??= new KeyboardController2();
                return _instance;
            }
        }

        private KeyboardController2() : base(
                leftKey: Key.Left,
                rightKey: Key.Right,
                upKey: Key.Up,
                downKey: Key.Down,
                undoKey: Key.NumPad1,
                screenSizeKey: Key.NumPad7,
                volumeDownKey: Key.PageDown,
                volumeUpKey: Key.PageUp,
                openChatKey: null,
                submitChatKey: null,
                hideChatKey: null
            )
        { }
    }
}
