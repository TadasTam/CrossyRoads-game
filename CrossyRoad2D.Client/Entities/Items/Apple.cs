using CrossyRoad2D.Client.Components.Xaml;
using CrossyRoad2D.Client.Decorator;
using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Entities.Strategy;
using CrossyRoad2D.Client.Extensions;
using CrossyRoad2D.Common.Enums;
using CrossyRoad2D.Common.MessageContents;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Common.Utils;
using CrossyRoad2D.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CrossyRoad2D.Client.Entities.Items
{
    public class Apple : Item
    {
        private ItemXamlComponent _appleXaml;

        public Apple(UIElement uiElement, Position position) : base(EntityType.Item)
        {
            RenderPriority = EntityRenderPriority.Default;
            Rectangle = new Rectangle(position);
            _appleXaml = new ItemXamlComponent(uiElement);
            _appleXaml.Rectangle = Rectangle;
            Console.WriteLine("Apple was generated flyweight");
        }

        public override void Render(Canvas canvas)
        {
            _appleXaml.Rectangle = Rectangle;
            _appleXaml.Render(canvas);
        }

        public override void Consume(PlayerEntity player)
        {
            if (player.MoveAlgorithm is not ISpecialAlgorithm)
            {
                player.SetMoveAlgo(new AppleAlgorithm());
                player.FrogDecorated = new PlayerRenderSpellDecorator(player.FrogDecorated, "Apples", DateTime.Now.AddSeconds(10));
            }
            
            Console.WriteLine("Apple was consumed");
            EntityCollection.Instance.RemoveEntity(this);
        }
    }

}
