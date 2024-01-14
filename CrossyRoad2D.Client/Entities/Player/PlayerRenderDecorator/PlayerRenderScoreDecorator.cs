using CrossyRoad2D.Client.Components.Xaml;
using CrossyRoad2D.Client.Entities;
using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Server.Models;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace CrossyRoad2D.Client.Decorator
{
    public class PlayerRenderScoreDecorator : PlayerRenderDecorator
    {
        public PlayerRenderScoreDecorator(IPlayerRender component, ServerPlayer player) : base(component)
        {
            Player = player;
        }
        private ServerPlayer Player { get; set; }

        public void Decorate(FrameworkElement frameworkElement)
        {
            var textBlock = frameworkElement.FindName("score") as TextBlock;
            if (textBlock != null)
            {
                textBlock.Text = Player.Position.Y.ToString();
            }
        }
        public override void Render(Canvas canvas, bool convertGridToRendered = true)
        {
            Decorate(uiElement as FrameworkElement);
            base.Render(canvas, convertGridToRendered);
        }

    }
}
