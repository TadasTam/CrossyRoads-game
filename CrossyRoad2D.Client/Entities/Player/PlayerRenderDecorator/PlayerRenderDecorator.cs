using CrossyRoad2D.Client.Components.Xaml;
using CrossyRoad2D.Client.Entities;
using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Server.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CrossyRoad2D.Client.Decorator
{
    public abstract class PlayerRenderDecorator : IPlayerRender
    {
        protected PlayerRenderDecorator(IPlayerRender component)
        {
            wrapee = component;
            uiElement = getUiElement();
        }

        public UIElement uiElement { get; init; }

        public IPlayerRender wrapee;

        public virtual void Render(Canvas canvas, bool convertGridToRendered = true)
        {
            wrapee.Render(canvas, convertGridToRendered);
        }

        public UIElement getUiElement()
        {
            return wrapee.getUiElement();
        }
    }
}
