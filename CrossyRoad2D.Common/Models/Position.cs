using CrossyRoad2D.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CrossyRoad2D.Common.Models
{
    public class Position : IEquatable<Position>
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Position()
        {
            X = 0.0;
            Y = 0.0;
        }

        public Position(Position other)
        {
            X = other.X;
            Y = other.Y;
        }

        public Position(double x, double y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Position other)
        {
            if (other == null)
                return false;

            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + X.GetHashCode();
                hash = hash * 23 + Y.GetHashCode();
                return hash;
            }
        }

        public static Position GetRandomPlayerStartingPosition()
        {
            var random = new Random();
            return new Position(
                Math.Floor(random.NextDouble() * GridUtils.TileCountX), 
                0.0);
        }
    }
}
