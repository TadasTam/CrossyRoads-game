using CrossyRoad2D.Client.Components.Xaml;
////using CrossyRoad2D.Client.Decorator.I;
using CrossyRoad2D.Client.Entities;
using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Extensions;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Server.Models;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CrossyRoad2D.Client.Decorator
{
    public class PlayerRenderBadCopyDecorator : PlayerRenderDecorator
    {
        public PlayerRenderBadCopyDecorator(IPlayerRender component) : base(component)
        {

        }

        public Color Color { get; set; } = new Color(1,0,0);

        public void Decorate(FrameworkElement frameworkElement)
        {
            var body = frameworkElement.FindName("body") as Ellipse;
            if (body != null)
            {
                body.Stroke = Color.ToWindowsMediaSolidColorBrush();
            }
        }

        public override void Render(Canvas canvas, bool convertGridToRendered = true)
        {
            Decorate(uiElement as FrameworkElement);
            base.Render(canvas, convertGridToRendered);
        }

    }
}
