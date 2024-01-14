using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Common.Models
{
    public class Item
    {
        public Color Color { get; set; }
        public Position Position { get; set; }
        public Rectangle Rectangle { get; set; } = new Rectangle();
    }
}
