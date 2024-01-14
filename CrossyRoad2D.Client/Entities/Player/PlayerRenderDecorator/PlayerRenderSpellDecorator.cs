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
    public class PlayerRenderSpellDecorator : PlayerRenderDecorator
    {
        public PlayerRenderSpellDecorator(IPlayerRender component, string label, DateTime expires) : base(component)
        {
            Expires = expires;
            Label = label;
        }

        private DateTime Expires { get; set; }
        private string Label { get; set; }
        public bool undecorate = false;

        public void Decorate(FrameworkElement frameworkElement)
        {
            if (Expires < DateTime.Now)
            {
                (frameworkElement.FindName("spellText") as TextBlock).Text = "";
                undecorate = true;
                return;
            }

            var textBlock = frameworkElement.FindName("spellText") as TextBlock;
            if (textBlock != null)
            {
                textBlock.Text = Label + ": " + (Expires - DateTime.Now).Seconds.ToString();
            }
        }

        public override void Render(Canvas canvas, bool convertGridToRendered = true)
        {
            if(undecorate == false)
                Decorate(uiElement as FrameworkElement);

            base.Render(canvas, convertGridToRendered);
        }

    }
}
