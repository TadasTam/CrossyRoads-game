using CrossyRoad2D.Client.Components.Xaml;
using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Singletons;
using CrossyRoad2D.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace CrossyRoad2D.Client.Entities.Obstacles
{
    public class ExplosionEntity : Entity, ILethalCollidable
    {
        private const double Duration = 0.75;
        private const double LethalDuration = 0.5;
        private double _startTime;
        private ExplosionXamlComponent _explosionXamlComponent;

        public ExplosionEntity(Position position) : base(EntityType.Explosion)
        {
            _startTime = TimeState.Instance.TimeSecondsSinceStart;
            _explosionXamlComponent = new ExplosionXamlComponent();
            _explosionXamlComponent.Rectangle = new Rectangle(position.X + 0.5, position.Y - 0.5, 2, 2);
        }

        public Rectangle GetCollisionRectangle()
        {
            if (TimeState.Instance.TimeSecondsSinceStart - _startTime > LethalDuration)
            {
                return new Rectangle(-1, -1, 0, 0);
            }

            return _explosionXamlComponent.Rectangle;
        }

        public override void PrioritizedUpdate()
        {
            if (TimeState.Instance.TimeSecondsSinceStart - _startTime > Duration)
            {
                EntityCollection.Instance.RemoveEntity(this);
            }
        }

        public override void Render(Canvas canvas)
        {
            _explosionXamlComponent.Render(canvas);
        }
    }
}
