using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Extensions;
using CrossyRoad2D.Common.Models;
using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace CrossyRoad2D.Client.Components.Xaml
{
    public class XamlComponent
    {
        public Rectangle Rectangle { get; set; } = new Rectangle();
        public double Scale { get; set; } = 1.0;
        public double Rotation { get; set; } = 0.0;
        public double Width { get; set; }
        public double Height { get; set; }

        protected UIElement _uiElement { get; init; }

        public XamlComponent(string fileName)
        {
            var stream = Application.GetResourceStream(new Uri($"pack://application:,,,/Resources/{fileName}")).Stream;
            using StreamReader reader = new(stream);
            string xamlContent = reader.ReadToEnd();
            _uiElement = XamlReader.Parse(xamlContent) as UIElement;
        }

        public XamlComponent(UIElement uiElement)
        {
            string uiClone = XamlWriter.Save(uiElement);
            _uiElement = XamlReader.Parse(uiClone) as UIElement;
        }

        public virtual void Render(Canvas canvas, bool convertGridToRendered = true)
        {
            var convertedRectangle = convertGridToRendered ? Rectangle.FromGridToRendered(canvas) : Rectangle;

            if (_uiElement is FrameworkElement frameworkElement)
            {
                var viewbox = frameworkElement.FindName("viewbox") as Viewbox;
                if (viewbox != null)
                {
                    viewbox.Width = convertedRectangle.Width;
                    viewbox.Height = convertedRectangle.Height;
                }

                var scaleTransform = frameworkElement.FindName("scaleTransform") as ScaleTransform;
                if (scaleTransform != null)
                {
                    scaleTransform.ScaleX = Scale;
                    scaleTransform.ScaleY = Scale;
                }

                var rotateTransform = frameworkElement.FindName("rotateTransform") as RotateTransform;
                if (rotateTransform != null)
                {
                    rotateTransform.Angle = Rotation;
                }

                OnXamlRender(frameworkElement);
            }

            canvas.Children.Add(_uiElement);
            Canvas.SetLeft(_uiElement, convertedRectangle.X);
            Canvas.SetTop(_uiElement, convertedRectangle.Y);
        }

        public virtual void OnXamlRender(FrameworkElement frameworkElement)
        {

        }
    }
}
