using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Entities.Obstacles;
using CrossyRoad2D.Client.Facade;
using CrossyRoad2D.Client.Iterator;
using CrossyRoad2D.Client.Singletons;
using CrossyRoad2D.Client.Utils;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.Player.State
{
    public class ExtraLifeState : IState
    {
        private PlayerEntity _player { get; set; }
        private double _runOutOn;

        const double EXTRA_LIFE_STATE_DURATION = 10.0;
        const double BONUS_DURATION_FROM_EXTRA_ITEM = 5.0;

        public ExtraLifeState(PlayerEntity player)
        {
            _player = player;
            _runOutOn = TimeState.Instance.TimeSecondsSinceStart + EXTRA_LIFE_STATE_DURATION;
        }

        public void ConsumeHeart()
        {
            _runOutOn += BONUS_DURATION_FROM_EXTRA_ITEM;
        }

        public void CheckCollisionsWithLethalCollidables()
        {
            Position newPosition = _player.ServerPlayer.Position;
            bool colliding = false;

            IIterator iterator = EntityCollection.Instance.createLethalEntityIterator();
            while (iterator.hasMore())
            {
                ILethalCollidable lethalCollidable = (ILethalCollidable)iterator.getNext();

                if (CollisionUtils.AreRectanglesColliding(lethalCollidable.GetCollisionRectangle(), new Rectangle(newPosition.X + 0.25, newPosition.Y + 0.25, 0.5, 0.5)))
                {
                    colliding = true;
                }
            }

            if (colliding)
            {
                _player.State = new ImmortalState(_player);
            }
        }
        
        public void UpdatePlayerState()
        {
            if (_runOutOn < TimeState.Instance.TimeSecondsSinceStart)
            {
                _player.State = new NormalState(_player);
            }
        }

        public string GetTimeRemainingString()
        {
            return $"Extra life: {(int)Math.Ceiling(_runOutOn - TimeState.Instance.TimeSecondsSinceStart)}";
        }
    }
}

