using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Extensions;
using CrossyRoad2D.Client.Singletons;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Common.Utils;
using CrossyRoad2D.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CrossyRoad2D.Client.Entities.Obstacles
{
    public class WallEntity : Entity, IUnpassable
    {
        public Rectangle Rectangle { get; set; } = new Rectangle();

        public WallEntity(Position position) : base(EntityType.Wall)
        {
            RenderPriority = EntityRenderPriority.Default;
            Rectangle = new Rectangle(position);
        }

        public override void Render(Canvas canvas)
        {
            var colouredRectangle = new ColouredRectangleEntity();

            colouredRectangle.Color = Color.White;
            colouredRectangle.Rectangle = Rectangle.FromGridToRendered(canvas);

            colouredRectangle.Render(canvas);
        }

        public Rectangle GetCollisionRectangle()
        {
            return Rectangle;
        }
    }
}
