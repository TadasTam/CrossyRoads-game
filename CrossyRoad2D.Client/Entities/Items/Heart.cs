using CrossyRoad2D.Client.Components.Xaml;
using CrossyRoad2D.Client.Decorator;
using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Entities.Strategy;
using CrossyRoad2D.Client.Extensions;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CrossyRoad2D.Client.Entities.Items
{
    public class Heart : Item
    {
        private ItemXamlComponent _heartXaml;

        public Heart(UIElement uiElement, Position position) : base(EntityType.Item)
        {
            RenderPriority = EntityRenderPriority.Default;
            Rectangle = new Rectangle(position);
            _heartXaml = new ItemXamlComponent(uiElement);
            _heartXaml.Rectangle = Rectangle;
            Console.WriteLine("Heart was created flyweight");
        }

        public override void Render(Canvas canvas)
        {
            _heartXaml.Rectangle = Rectangle;
            _heartXaml.Render(canvas);
        }

        public override void Consume(PlayerEntity player)
        {
            EntityCollection.Instance.RemoveEntity(this);
            player.State.ConsumeHeart();
        }
    }
}
