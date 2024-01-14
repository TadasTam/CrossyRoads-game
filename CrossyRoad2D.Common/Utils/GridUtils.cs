using CrossyRoad2D.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Common.Utils
{
    public static class GridUtils
    {
        public const int TileMaxLength = 120;
        public const int TileCountX = 20;

        public static bool IsPositionOutOfBounds(Rectangle rectangle)
        {
            return rectangle.X < 0 || rectangle.X >= TileCountX || rectangle.Y < 0;
        }

        public static bool IsPositionOutOfBounds(Position position)
        {
            return IsPositionOutOfBounds(new Rectangle(position));
        }

        public static Rectangle GetRenderMetrics(Rectangle gridMetrics, int screenWidth, int screenHeight, Rectangle camera = null)
        {
            int tileLength = Math.Min(screenWidth / TileCountX, TileMaxLength);
            int tileAreaWidth = tileLength * TileCountX;
            int gridStartX = (screenWidth - tileAreaWidth) / 2; // wtf

            var renderMetrics = new Rectangle(gridStartX + tileLength * gridMetrics.X,
                screenHeight - tileLength * gridMetrics.Y - gridMetrics.Height * tileLength,
                gridMetrics.Width * tileLength,
                gridMetrics.Height * tileLength);

            if(camera is not null)
            {
                return TransformForCamera(renderMetrics, camera, screenWidth, screenHeight);
            } else
            {
                return renderMetrics;
            }
        }

        private static Rectangle TransformForCamera(Rectangle renderRect, Rectangle cameraRect, int screenWidth, int screenHeight)
        {
            double translatedX = renderRect.X - cameraRect.X;
            double translatedY = renderRect.Y - cameraRect.Y;

            double scaleX = screenWidth / cameraRect.Width;
            double scaleY = screenHeight / cameraRect.Height;

            int scaledWidth = (int)(renderRect.Width * scaleX);
            int scaledHeight = (int)(renderRect.Height * scaleY);

            translatedX = (int)(translatedX * scaleX);
            translatedY = (int)(translatedY * scaleY);

            return new Rectangle(translatedX, translatedY, scaledWidth, scaledHeight);
        }
    }
}
