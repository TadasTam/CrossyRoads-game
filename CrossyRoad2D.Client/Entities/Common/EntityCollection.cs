using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CrossyRoad2D.Client.Entities;
using CrossyRoad2D.Client.Entities.ItemsFactory;
using CrossyRoad2D.Client.Entities.Player;
using CrossyRoad2D.Client.Entities.Obstacles;
using CrossyRoad2D.Common.Models;
using CrossyRoad2D.Client.Entities.SpawnAreas;
using CrossyRoad2D.Client.Commands;
using CrossyRoad2D.Client.Entities.Items;
using CrossyRoad2D.Client.Entities.Chat;
using CrossyRoad2D.Client.Iterator;
using CrossyRoad2D.Client.NetworkSync;

namespace CrossyRoad2D.Client.Entities.Common
{
    public class EntityCollection : IterableEntityCollection
    {
        private static EntityCollection _instance;
        public static EntityCollection Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new EntityCollection();
                }
                return _instance;
            }
        }

        private List<Entity> _entities { get; set; } = new List<Entity>();
        private NetworkSyncedEntityVisitor _networkSyncedVisitor = new();

        private EntityCollection()
        {
            AddEntity(new ItemSpawner());
            AddEntity(new PlayerSpawnerEntity());
            AddEntity(new ChatEntity());
            AddEntity(new WallEntity(new Position(3, 9)));
            AddEntity(new WallEntity(new Position(5, 9)));
            AddEntity(new SandEntity(new Position(16, 1)));
            AddEntity(new SandEntity(new Position(17, 1)));
            AddEntity(new SandEntity(new Position(18, 1)));
            AddEntity(new SandEntity(new Position(19, 1)));
            AddEntity(new SandEntity(new Position(15, 1)));
            AddEntity(new SandEntity(new Position(19, 0)));
            AddEntity(new SandEntity(new Position(18, 0)));
            AddEntity(new SandEntity(new Position(17, 0)));
            AddEntity(new RoadEntity(positionY: 6, new List<SpawnableWithChance>()
            {
                new SpawnableWithChance()
                {
                    GetSpawnable = (positionY, fromLeft) =>
                    {
                        return new CarEntity(positionY, fromLeft);
                    },
                    Chance = 0.8
                },
                new SpawnableWithChance()
                {
                    GetSpawnable = (positionY, fromLeft) =>
                    {
                        return new TruckEntity(positionY, fromLeft);
                    },
                    Chance = 0.2
                },
            }));
            AddEntity(new GravelRoadEntity(positionY: 3, new List<SpawnableWithChance>()
            {
                new SpawnableWithChance()
                {
                    GetSpawnable = (positionY, fromLeft) =>
                    {
                        return new TruckEntity(positionY, fromLeft);
                    },
                    Chance = 0.3
                },
                new SpawnableWithChance()
                {
                    GetSpawnable = (positionY, fromLeft) =>
                    {
                        return new CarEntity(positionY, fromLeft);
                    },
                    Chance = 0.7
                }
            }));

            CommandInvoker.Instance.Run(new ConnectCommand());
        }


        public void RenderEntities(Canvas canvas)
        {
            _entities
                .OrderByDescending(entity => entity.RenderPriority)
                .ToList()
                .ForEach(entity => entity.Render(canvas));
        }

        public void UpdateEntitiesPrioritized()
        {
            var orderedEntities = _entities.OrderByDescending(entity => entity.UpdatePriority);

            foreach(var entity in orderedEntities)
            {
                entity.PrioritizedUpdate();
            }

            foreach (var entity in orderedEntities)
            {
                if (entity is INetworkSyncedEntity)
                {
                    (entity as INetworkSyncedEntity).Accept(_networkSyncedVisitor);
                }
            }
        }

        public int GetNextCloneId(string playerServerId)
        {
            return _entities.Where(entity => entity.Type == EntityType.Player)
                        .Where(entity => (entity as PlayerEntity).GetPlayerServerId() == playerServerId)
                        .Max(entity => (entity as PlayerEntity).GetPlayerEntityCopyId()) + 1;
        }

        public void UpdateEntity(Entity entity)
        {
            _entities = _entities
                .Select(iterationEntity => iterationEntity.Id == entity.Id ? entity : iterationEntity)
                .ToList();
        }

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }

        public void RemoveEntity(Entity entity)
        {
            _entities
                .ToList()
                .ForEach(iterationEntity =>
                {
                    if (entity.Id == iterationEntity.Id)
                    {
                        iterationEntity.WillBeRemovedNextFrame = true;
                    }
                });
        }

        public void RemoveEntityById(long Id)
        {
            _entities
                .ToList()
                .ForEach(iterationEntity =>
                {
                    if (Id == iterationEntity.Id)
                    {
                        iterationEntity.WillBeRemovedNextFrame = true;
                    }
                });
        }

        public void RemovePlayerEntities(string playerServerId)
        {
            _entities
                .Where(entity => entity.Type == EntityType.Player)
                .ToList()
                .ForEach(iterationEntity =>
                {
                    if ((iterationEntity as PlayerEntity).GetPlayerServerId() == playerServerId) 
                    {
                        iterationEntity.WillBeRemovedNextFrame = true;
                    }
                });
        }

        public void FinishRemovingEntities()
        {
            _entities = _entities
                .Where(entity => entity.WillBeRemovedNextFrame == false)
                .ToList();
        }

        public IIterator createIterator(EntityType type)
        {
            return new EntityIterator(_entities, type);
        }

        public IIterator createLethalEntityIterator()
        {
            return new LethalEntityIterator(_entities);
        }

        public IIterator createUnpassableEntityIterator()
        {
            return new UnpassableEntityIterator(_entities);
        }

        public IIterator createTruckCollidableEntityIterator()
        {
            return new TruckCollidableEntityIterator(_entities);
        }

        public List<Entity> GetEntitiesOfType(EntityType type)
        {
            List<Entity> entities = new();
            IIterator iterator = createIterator(type);
            while (iterator.hasMore())
            {
                Entity current = iterator.getNext();
                entities.Add(current);
            }

            return entities;
        }
    }
}
