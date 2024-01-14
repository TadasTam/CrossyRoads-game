using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Extensions;
using CrossyRoad2D.Common.Models;
using System.Windows.Controls;

namespace CrossyRoad2D.Client.Components
{
    public class ColouredRectangleComponent
    {
        public Rectangle Rectangle { get; set; } = new Rectangle();
        public Color Color { get; set; } = new Color(1.0f, 1.0f, 1.0f);

        public ColouredRectangleComponent()
        {
        }

        public void Render(Canvas canvas, bool convertGridToRendered = true)
        {
            var convertedRectangle = convertGridToRendered ? Rectangle.FromGridToRendered(canvas) : Rectangle;

            System.Windows.Shapes.Rectangle rect = new()
            {
                Width = convertedRectangle.Width,
                Height = convertedRectangle.Height,
                Fill = Color.ToWindowsMediaSolidColorBrush(),
            };

            canvas.Children.Add(rect);
            Canvas.SetLeft(rect, convertedRectangle.X);
            Canvas.SetTop(rect, convertedRectangle.Y);
        }
    }
}
