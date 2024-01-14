using CrossyRoad2D.Client.Decorator;
using CrossyRoad2D.Client.Extensions;
using CrossyRoad2D.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace CrossyRoad2D.Client.Components.Xaml
{
    public class FrogXamlComponent : XamlComponent, IPlayerRender
    {
        public Color Color { get; set; } = new Color(1.0f, 1.0f, 1.0f);

        public FrogXamlComponent() : base("frog.xaml")
        {

        }

        public override void OnXamlRender(FrameworkElement frameworkElement)
        {
            var frogBody = frameworkElement.FindName("body") as Ellipse;
            if (frogBody != null)
            {
                frogBody.Fill = Color.ToWindowsMediaSolidColorBrush();
            }
        }

        public UIElement getUiElement()
        {
            return _uiElement;
        }
    }
}
