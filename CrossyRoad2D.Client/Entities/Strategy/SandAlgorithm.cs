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

namespace CrossyRoad2D.Client.Entities.Strategy
{
    public class SandAlgorithm : MoveAlgorithm
    {
        protected override double GetMinMoveActionInterval()
        {
            return 0.6;
        }

        protected override bool CanMove(Position newPosition, Position oldPosition, PlayerEntity entity)
        {
            return !oldPosition.Equals(newPosition) &&
                !GridUtils.IsPositionOutOfBounds(newPosition);
        }

        protected override void PerformMove(Position newPosition, PlayerEntity entity)
        {
            if (!FacadeUtils.Instance.IsNextPositionSand(newPosition))
            {
                entity.SetMoveAlgo(new DefaultAlgorithm());
                Console.WriteLine("Exited sand environment");
            }
            entity.SetPosition(newPosition);
        }

        public override bool IsCollidingWithUnpassable(Position newPosition, PlayerEntity pEntity)
        {
            return FacadeUtils.Instance.IsCollidingWithUnpassable(newPosition, pEntity);
        }
    }
}
