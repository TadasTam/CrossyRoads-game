using CrossyRoad2D.Client.Facade;
using CrossyRoad2D.Client.Singletons;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Common.Utils;
using CrossyRoad2D.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.Strategy
{
    public abstract class MoveAlgorithm
    {
        public bool TemplateMethod(double offsetX, double offsetY, PlayerEntity entity, bool ignoreInterval = false)
        {
            entity.MinMoveActionIntervalSeconds = GetMinMoveActionInterval();

            if (entity.MinMoveActionIntervalSeconds == 0.3)
            {
                entity.CanWalkThroughObjects = false;
            }

            Console.WriteLine($"{GetType().Name} walking mode enabled");

            var oldPosition = new Position(entity.ServerPlayer.Position);
            var newPosition = new Position(entity.ServerPlayer.Position);

            if (entity.LastMoveActionTime + entity.MinMoveActionIntervalSeconds < TimeState.Instance.TimeSecondsSinceStart || ignoreInterval)
            {
                newPosition.X += offsetX;
                newPosition.Y += offsetY;
            }

            if (CanMove(newPosition, oldPosition, entity) && !IsCollidingWithUnpassable(newPosition, entity))
            {
                PerformMove(newPosition, entity);
                return true;
            }

            return false;
        }

        protected abstract double GetMinMoveActionInterval();

        protected abstract bool CanMove(Position newPosition, Position oldPosition, PlayerEntity entity);

        protected abstract void PerformMove(Position newPosition, PlayerEntity entity);

        public abstract bool IsCollidingWithUnpassable(Position newPosition, PlayerEntity pEntity);
    }
}
