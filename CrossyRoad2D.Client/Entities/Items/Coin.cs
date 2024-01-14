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
    public class Coin : Item
    {
        private ItemXamlComponent _coinXaml;

        public Coin(UIElement uiElement, Position position) : base(EntityType.Item)
        {
            RenderPriority = EntityRenderPriority.Default;
            Rectangle = new Rectangle(position);
            _coinXaml = new ItemXamlComponent(uiElement);
            _coinXaml.Rectangle = Rectangle;
            Console.WriteLine("Coin was created flyweight");
        }

        public override void Render(Canvas canvas)
        {
            _coinXaml.Rectangle = Rectangle;
            _coinXaml.Render(canvas);
        }

        public override void Consume(PlayerEntity player)
        {
            player.PerformCloneDeep();
            Console.WriteLine("Coin was consumed");
            EntityCollection.Instance.RemoveEntity(this);
        }
    }
}
