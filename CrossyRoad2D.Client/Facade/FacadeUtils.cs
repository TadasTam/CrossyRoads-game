using CrossyRoad2D.Client.Entities;
using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Entities.Obstacles;
using CrossyRoad2D.Client.Entities.SpawnAreas;
using CrossyRoad2D.Client.Iterator;
using CrossyRoad2D.Client.NetworkSync;
using CrossyRoad2D.Client.Utils;
using CrossyRoad2D.Common.Enums;
using CrossyRoad2D.Common.MessageContents;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Server.Models;
using Item = CrossyRoad2D.Client.Entities.Items.Item;

namespace CrossyRoad2D.Client.Facade
{
    public class FacadeUtils
    {
        private static FacadeUtils _instance;
        public static FacadeUtils Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new FacadeUtils();
                }
                return _instance;
            }
        }

        public void ClonePlayer(PlayerEntity clone)
        {
            EntityCollection.Instance.AddEntity(clone);
        }

        public int GetNextCloneId(string serverId)
        {
            return EntityCollection.Instance.GetNextCloneId(serverId);
        }

        public void Die(bool IsOfCurrentClient, long Id, ServerPlayer serverPlayer)
        {
            if (IsOfCurrentClient)
            {
                var fullyDied = true;
                if (serverPlayer.IsBadCopy == false)
                {
                    IIterator iterator = EntityCollection.Instance.createIterator(EntityType.Player);
                    while (iterator.hasMore())
                    {
                        PlayerEntity player = (PlayerEntity)iterator.getNext();

                        if (player.IsOfCurrentClient == true && player?.Id != Id)
                        {
                            fullyDied = false;
                        }
                    }
                }

                if (fullyDied)
                {
                    EntityCollection.Instance.RemovePlayerEntities(serverPlayer.PlayerServerId);
                }
                else
                {
                    EntityCollection.Instance.RemoveEntityById(Id);
                }
            }
        }

        public void ConsumeItems(Position newPosition, PlayerEntity player)
        {
            IIterator iterator = EntityCollection.Instance.createIterator(EntityType.Item);
            while (iterator.hasMore())
            {
                Item item = (Item)iterator.getNext();

                if (CollisionUtils.AreRectanglesColliding(item.Rectangle, new Rectangle(newPosition)))
                {
                    item.Consume(player);
                }
            }
        }

        public void TruckCollide(Rectangle truckRectangle)
        {
            IIterator iterator = EntityCollection.Instance.createTruckCollidableEntityIterator();
            while (iterator.hasMore())
            {
                Entity entity = (Entity)iterator.getNext();
                IDestroyableByTruck lethalCollidable = (IDestroyableByTruck)entity;
                var collisionRectangle = lethalCollidable.GetCollisionRectangle();

                if (CollisionUtils.AreRectanglesColliding(collisionRectangle, truckRectangle))
                {
                    EntityCollection.Instance.RemoveEntity(entity);
                    EntityCollection.Instance.AddEntity(new ExplosionEntity(new Position(collisionRectangle.X, collisionRectangle.Y)));
                }
            }
        }

        public bool IsCollidingWithUnpassable(Position newPosition, PlayerEntity player)
        {
            bool colliding = false;

            IIterator iterator = EntityCollection.Instance.createUnpassableEntityIterator();
            while (iterator.hasMore())
            {
                IUnpassable wall = (IUnpassable)iterator.getNext();

                if (CollisionUtils.AreRectanglesColliding(wall.GetCollisionRectangle(), new Rectangle(newPosition)) && !player.CanWalkThroughObjects)
                {
                    colliding = true;
                }
            }

            return colliding;
        }

        public bool IsNextPositionSand(Position newPosition)
        {
            bool isNextSand = false;
            
            IIterator iterator = EntityCollection.Instance.createIterator(EntityType.Sand);
            while (iterator.hasMore())
            {
                SandEntity sand = (SandEntity)iterator.getNext();

                if (CollisionUtils.AreRectanglesColliding(sand.GetCollisionRectangle(), new Rectangle(newPosition)))
                {
                    isNextSand = true;
                }
            }

            return isNextSand;
        }
    }
}
