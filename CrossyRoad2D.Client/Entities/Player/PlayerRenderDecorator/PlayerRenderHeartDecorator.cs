﻿using CrossyRoad2D.Client.Components.Xaml;
////using CrossyRoad2D.Client.Decorator.I;
using CrossyRoad2D.Client.Entities;
using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Entities.Player.State;
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
    public class PlayerRenderHeartDecorator : PlayerRenderDecorator
    {
        public PlayerRenderHeartDecorator(IPlayerRender component, PlayerEntity player) : base(component)
        {
            playerReference = player;
        }

        private PlayerEntity playerReference;

        public void Decorate(FrameworkElement frameworkElement)
        {
            var textBlock = frameworkElement.FindName("heartText") as TextBlock;
            textBlock.Text = playerReference.State.GetTimeRemainingString();
        }

        public override void Render(Canvas canvas, bool convertGridToRendered = true)
        {
            Decorate(uiElement as FrameworkElement);
            base.Render(canvas, convertGridToRendered);
        }
    }
}
