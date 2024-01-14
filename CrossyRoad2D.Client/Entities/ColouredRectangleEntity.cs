using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Extensions;
using CrossyRoad2D.Common.Models;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace CrossyRoad2D.Client.Entities
{
    public class ColouredRectangleEntity : Entity
    {
        public CrossyRoad2D.Common.Models.Rectangle Rectangle { get; set; } = new();
        public Color Color { get; set; } = new Color(1.0f, 1.0f, 1.0f);

        public ColouredRectangleEntity(EntityType entityType) : base(entityType)
        {
        }

        public ColouredRectangleEntity() : base(EntityType.ColouredRectangle)
        {
        }

        public override void Render(Canvas canvas)
        {
            System.Windows.Shapes.Rectangle rect = new System.Windows.Shapes.Rectangle
            {
                Width = Rectangle.Width,
                Height = Rectangle.Height,
                Fill = Color.ToWindowsMediaSolidColorBrush(),
            };

            canvas.Children.Add(rect);
            Canvas.SetLeft(rect, Rectangle.X);
            Canvas.SetTop(rect, Rectangle.Y);
        }

        public override void PrioritizedUpdate()
        {

        }
    }
}
