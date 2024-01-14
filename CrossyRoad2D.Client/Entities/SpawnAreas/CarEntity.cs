using CrossyRoad2D.Client.Components.Xaml;
using CrossyRoad2D.Client.Entities.Obstacles;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CrossyRoad2D.Client.Entities.SpawnAreas
{
    public class CarEntity : Spawnable, IDestroyableByTruck, ILethalCollidable
    {
        private CarXamlComponent _carXaml;

        public CarEntity(double positionY, bool isStartLeft) : base(positionY, isStartLeft, 4)
        {
            _carXaml = new CarXamlComponent();
            _carXaml.Rotation = isStartLeft ? 0.0 : 180.0;
            _carXaml.Color = Color.GetRandomStartingCarColor();
            _carXaml.Rectangle = Rectangle;
        }

        public Rectangle GetCollisionRectangle()
        {
            return _carXaml.Rectangle;
        }

        public override void Render(Canvas canvas)
        {
            _carXaml.Rectangle = Rectangle;
            _carXaml.Render(canvas);
        }
    }
}
