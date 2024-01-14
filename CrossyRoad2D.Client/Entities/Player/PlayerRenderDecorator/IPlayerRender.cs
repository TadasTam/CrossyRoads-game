using CrossyRoad2D.Client.Components.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CrossyRoad2D.Client.Decorator
{
    public interface IPlayerRender
    {
        public void Render(Canvas canvas, bool convertGridToRendered = true);
        public UIElement getUiElement();
    }
}
