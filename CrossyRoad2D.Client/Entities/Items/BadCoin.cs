using CrossyRoad2D.Client.Components.Xaml;
using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Entities.Strategy;
using CrossyRoad2D.Client.Extensions;
using CrossyRoad2D.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CrossyRoad2D.Client.Entities.Items
{
    public class BadCoin : Item
    {
        private ItemXamlComponent _badCoinXaml;

        public BadCoin(UIElement uiElement, Position position) : base(EntityType.Item)
        {
            RenderPriority = EntityRenderPriority.Default;
            Rectangle = new Rectangle(position);
            _badCoinXaml = new ItemXamlComponent(uiElement);
            _badCoinXaml.Rectangle = Rectangle;
            Console.WriteLine("BadCoin was created flyweight");
        }

        public override void Render(Canvas canvas)
        {
            _badCoinXaml.Rectangle = Rectangle;
            _badCoinXaml.Render(canvas);
        }

        public override void Consume(PlayerEntity player)
        {
            player.PerformCloneShallow();
            Console.WriteLine("BadCoin was consumed");
            EntityCollection.Instance.RemoveEntity(this);
        }
    }
}
