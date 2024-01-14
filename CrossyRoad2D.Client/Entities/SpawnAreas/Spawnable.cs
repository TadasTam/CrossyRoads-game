using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Singletons;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Common.Utils;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.SpawnAreas
{
    public abstract class Spawnable : Entity
    {
        public Rectangle Rectangle { get; set; }
        protected double _speed;
        protected bool _isStartLeft;

        protected Spawnable(double positionY, bool isStartLeft, double speed) : base(EntityType.Spawnable)
        {
            Rectangle = new Rectangle(isStartLeft ? -2 : GridUtils.TileCountX + 2, positionY, 2, 1);
            _speed = speed;
            _isStartLeft = isStartLeft;
        }

        public override void PrioritizedUpdate()
        {
            Rectangle.X = Rectangle.X + _speed * TimeState.Instance.TimeDeltaSeconds * (_isStartLeft ? 1 : -1);

            if(Rectangle.X < -5 || Rectangle.X > GridUtils.TileCountX + 5)
            {
                EntityCollection.Instance.RemoveEntity(this);
            }
        }
    }
}
