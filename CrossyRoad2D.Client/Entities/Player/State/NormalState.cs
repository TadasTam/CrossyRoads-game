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
    public class NormalState : IState
    {
        private PlayerEntity _player { get; set; }

        public NormalState(PlayerEntity player)
        {
            _player = player;
        }

        public void ConsumeHeart()
        {
            _player.State = new ExtraLifeState(_player);
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
                FacadeUtils.Instance.Die(_player.IsOfCurrentClient, _player.Id, _player.ServerPlayer);
            }
        }

        public void UpdatePlayerState()
        {
        }

        public string GetTimeRemainingString()
        {
            return "";
        }
    }
}
