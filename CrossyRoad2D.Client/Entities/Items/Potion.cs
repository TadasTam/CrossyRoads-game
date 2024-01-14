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
    public class Potion : Item
    {
        private ItemXamlComponent _potionXaml;

        public Potion(UIElement uiElement, Position position) : base(EntityType.Item)
        {
            RenderPriority = EntityRenderPriority.Default;
            Rectangle = new Rectangle(position);
            _potionXaml = new ItemXamlComponent(uiElement);
            _potionXaml.Rectangle = Rectangle;
            Console.WriteLine("Potion was created flyweight");
        }

        public override void Render(Canvas canvas)
        {
            _potionXaml.Rectangle = Rectangle;
            _potionXaml.Render(canvas);
        }

        public override void Consume(PlayerEntity player)
        {
            if (player.MoveAlgorithm is not ISpecialAlgorithm)
            {
                player.SetMoveAlgo(new PotionAlgorithm());
                player.FrogDecorated = new PlayerRenderSpellDecorator(player.FrogDecorated, "Walls", DateTime.Now.AddSeconds(10));
            }
            Console.WriteLine("Potion was consumed");
            EntityCollection.Instance.RemoveEntity(this);
        }
    }
}
