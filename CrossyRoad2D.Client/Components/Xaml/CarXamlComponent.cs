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

namespace CrossyRoad2D.Client.Components.Xaml
{
    public class CarXamlComponent : XamlComponent
    {
        public Color Color { get; set; } = new Color(1.0f, 1.0f, 1.0f);

        public CarXamlComponent() : base("car.xaml")
        {
        }

        public override void OnXamlRender(FrameworkElement frameworkElement)
        {
            var carBody = frameworkElement.FindName("body") as System.Windows.Shapes.Rectangle;
            if (carBody != null)
            {
                carBody.Fill = Color.ToWindowsMediaSolidColorBrush();
            }
        }
    }
}
