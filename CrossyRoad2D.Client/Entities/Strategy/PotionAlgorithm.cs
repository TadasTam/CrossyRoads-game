using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Entities.Obstacles;
using CrossyRoad2D.Client.Singletons;
using CrossyRoad2D.Client.Utils;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Common.Utils;
using CrossyRoad2D.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Xps.Serialization;

namespace CrossyRoad2D.Client.Entities.Strategy
{
    public class PotionAlgorithm : MoveAlgorithm
    {
        protected override double GetMinMoveActionInterval()
        {
            return 0.3; // No minimum move action interval for PotionAlgorithm
        }

        protected override bool CanMove(Position newPosition, Position oldPosition, PlayerEntity entity)
        {
            return !oldPosition.Equals(newPosition) &&
                !GridUtils.IsPositionOutOfBounds(newPosition);
        }

        protected override void PerformMove(Position newPosition, PlayerEntity entity)
        {
            entity.SetPosition(newPosition);
        }

        public override bool IsCollidingWithUnpassable(Position newPosition, PlayerEntity pEntity)
        {
            return false;
        }
    }
}
