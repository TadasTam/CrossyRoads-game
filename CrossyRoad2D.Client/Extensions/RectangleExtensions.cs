using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CrossyRoad2D.Client.Extensions
{
    public static class RectangleExtensions
    {
        public static Rectangle FromGridToRendered(this Rectangle gridRectangle, Canvas canvas)
        {
            int screenWidth = Math.Clamp((int)canvas.ActualWidth, 100, 10000);
            int screenHeight = Math.Clamp((int)canvas.ActualHeight, 100, 10000);
            return GridUtils.GetRenderMetrics(gridRectangle, screenWidth, screenHeight);
        }
    }
}
