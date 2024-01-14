using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CrossyRoad2D.Client.Commands;
using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Entities;
using CrossyRoad2D.Client.Singletons.Controllers;

namespace CrossyRoad2D.Client.Singletons
{
    public class KeyboardController1 : BaseController
    {
        private static KeyboardController1 _instance;
        public static KeyboardController1 Instance
        {
            get
            {
                _instance ??= new KeyboardController1();
                return _instance;
            }
        }

        private KeyboardController1() : base(
                leftKey: Key.A,
                rightKey: Key.D,
                upKey: Key.W,
                downKey: Key.S,
                undoKey: Key.U,
                screenSizeKey: Key.P,
                volumeDownKey: Key.N,
                volumeUpKey: Key.M,
                openChatKey: Key.T,
                submitChatKey: Key.Enter,
                hideChatKey: Key.Escape
            )
        { }
    }
}
