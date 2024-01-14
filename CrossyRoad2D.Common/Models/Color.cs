using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Common.Models
{
    public class Color
    {
        public float Red { get; init; }
        public float Green { get; init; }
        public float Blue { get; init; }
        public float Alpha { get; init; }

        public Color(float red, float green, float blue, float alpha = 1.0f)
        {
            Red = red;
            Green = green;
            Blue = blue;
            Alpha = alpha;
        }

        public static Color White = new Color(1.0f, 1.0f, 1.0f);
        public static Color Black = new Color(0.0f, 0.0f, 0.0f);

        public static Color GetRandomStartingPlayerColor()
        {
            var random = new Random();
            return new Color((float)random.NextDouble(), 
                (float)random.NextDouble(), 
                (float)random.NextDouble());
        }

        public static Color GetRandomStartingCarColor()
        {
            var random = new Random();

            var color = Color.White;
            while(Math.Abs(color.Red - color.Green) + Math.Abs(color.Red - color.Blue) < 0.2)
            {
                color = new Color((float)random.NextDouble(),
                    (float)random.NextDouble(),
                    (float)random.NextDouble());
            }
            return color;
        }
    }
}
