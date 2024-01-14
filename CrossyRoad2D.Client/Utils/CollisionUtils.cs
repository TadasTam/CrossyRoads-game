using CrossyRoad2D.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Utils
{
    public static class CollisionUtils
    {
        public static bool AreRectanglesColliding(Rectangle rectangle1, Rectangle rectangle2)
        {
            return rectangle1.X < rectangle2.X + rectangle2.Width &&
                rectangle1.X + rectangle1.Width > rectangle2.X &&
                rectangle1.Y < rectangle2.Y + rectangle2.Height &&
                rectangle1.Y + rectangle1.Height > rectangle2.Y;
        }
    }
}
