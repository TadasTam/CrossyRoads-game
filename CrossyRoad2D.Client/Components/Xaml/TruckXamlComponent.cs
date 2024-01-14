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
    public class TruckXamlComponent : XamlComponent
    {
        public Color Color { get; set; } = new Color(1.0f, 1.0f, 1.0f);

        public TruckXamlComponent() : base("truck.xaml")
        {
        }

        public override void OnXamlRender(FrameworkElement frameworkElement)
        {
            var front1 = frameworkElement.FindName("front1") as System.Windows.Shapes.Rectangle;
            if (front1 != null)
            {
                front1.Fill = Color.ToWindowsMediaSolidColorBrush();
            }

            var front2 = frameworkElement.FindName("front2") as System.Windows.Shapes.Rectangle;
            if (front2 != null)
            {
                front2.Fill = Color.ToWindowsMediaSolidColorBrush();
            }
        }
    }
}
