using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Extensions;
using CrossyRoad2D.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CrossyRoad2D.Client.Entities.Obstacles
{
    public class SandEntity : Entity
    {
        public Rectangle Rectangle { get; set; } = new Rectangle();

        public SandEntity(Position position) : base(EntityType.Sand)
        {
            RenderPriority = EntityRenderPriority.Default;
            Rectangle = new Rectangle(position);
        }

        public override void Render(Canvas canvas)
        {
            var colouredRectangle = new ColouredRectangleEntity();

            colouredRectangle.Color = new Color(1.0f, 1.0f, 0.6f);
            colouredRectangle.Rectangle = Rectangle.FromGridToRendered(canvas);

            colouredRectangle.Render(canvas);
        }

        public Rectangle GetCollisionRectangle()
        {
            return Rectangle;
        }
    }
}
