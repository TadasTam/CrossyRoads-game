using CrossyRoad2D.Client.Components.Xaml;
using CrossyRoad2D.Client.Entities.Obstacles;
using CrossyRoad2D.Client.Facade;
using CrossyRoad2D.Client.Singletons;
using CrossyRoad2D.Client.Utils;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CrossyRoad2D.Client.Entities.SpawnAreas
{
    public class TruckEntity : Spawnable, ILethalCollidable
    {
        private TruckXamlComponent _truckXaml;

        public TruckEntity(double positionY, bool isStartLeft) : base(positionY, isStartLeft, 12)
        {
            _truckXaml = new TruckXamlComponent();
            _truckXaml.Rotation = isStartLeft ? 0.0 : 180.0;
            _truckXaml.Color = Color.GetRandomStartingCarColor();
            _truckXaml.Rectangle = Rectangle;
        }

        public override void Render(Canvas canvas)
        {
            _truckXaml.Render(canvas);
        }

        public Rectangle GetCollisionRectangle()
        {
            _truckXaml.Rectangle = Rectangle;
            return _truckXaml.Rectangle;
        }

        public override void PrioritizedUpdate()
        {
            base.PrioritizedUpdate();

            FacadeUtils.Instance.TruckCollide(_truckXaml.Rectangle);
        }
    }
}
