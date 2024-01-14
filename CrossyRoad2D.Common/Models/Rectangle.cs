using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Common.Models
{
    public class Rectangle
    {
        public double X { get; set; } = 0;
        public double Y { get; set; } = 0;
        public double Width { get; set; } = 1;
        public double Height { get; set; } = 1;

        public Rectangle()
        {

        }

        public Rectangle(Rectangle other)
        {
            X = other.X;
            Y = other.Y;
            Width = other.Width;
            Height = other.Height;
        }

        public Rectangle(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Rectangle(double x, double y)
        {
            X = x;
            Y = y;
        }

        public Rectangle(Position pos)
        {
            X = pos.X;
            Y = pos.Y;
        }

        public bool Equals(Rectangle other)
        {
            if (other == null)
                return false;

            return X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                hash = hash * 23 + Width.GetHashCode();
                hash = hash * 23 + Height.GetHashCode();
                return hash;
            }
        }
    }
}
