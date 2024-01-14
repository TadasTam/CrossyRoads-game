using CrossyRoad2D.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace CrossyRoad2D.Client.Components.Xaml
{
    public class RoadXamlComponent : XamlComponent
    {
        public RoadXamlComponent() : base("doubleRoad.xaml")
        {
            var frameworkElement = _uiElement as FrameworkElement;
            var roadGridResource = frameworkElement.Resources["RoadGrid"];
            var horizontalStackPanel = frameworkElement.FindName("horizontalStackPanel") as StackPanel;

            for (int i = 0; i < GridUtils.TileCountX; i++)
            {
                var clonedRoadGrid = XamlReader.Parse(XamlWriter.Save(roadGridResource)) as Grid;

                ContentPresenter contentPresenter = new ContentPresenter
                {
                    Width = 200,
                    Content = clonedRoadGrid
                };

                horizontalStackPanel.Children.Add(contentPresenter);
            }

            var viewBox = frameworkElement.FindName("viewBox") as Viewbox;
        }
    }
}
