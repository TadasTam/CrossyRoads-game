using CrossyRoad2D.Client.Entities.Player;
using CrossyRoad2D.Client.NetworkSync;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CrossyRoad2D.Client.Entities.Common
{
    public abstract class Entity : ICloneable<Entity>
    {
        public long Id { get; private set; }
        public EntityType Type { get; init; }
        public EntityUpdatePriority UpdatePriority { get; set; } = EntityUpdatePriority.Default;
        public EntityRenderPriority RenderPriority { get; set; } = EntityRenderPriority.Default;
        public bool WillBeRemovedNextFrame { get; set; } = false;

        private static int _lastId = 0;

        public Entity(
            EntityType entityType)
        {
            Id = _lastId;
            _lastId += 1;
            Type = entityType;
        }

        public void IncrementShallowCopyId()
        {
            Id = _lastId;
            _lastId += 1;
        }

        /// <summary>
        /// Entities with higher UpdatePriority gets their FixedPrioritizedUpdate called first.
        /// </summary>
        public virtual void PrioritizedUpdate()
        {

        }

        public virtual void Render(Canvas canvas)
        {

        }

        public virtual Entity CloneDeep()
        {
            return (Entity)MemberwiseClone();
        }

        public virtual Entity CloneShallow()
        {
            return (Entity)MemberwiseClone();
        }
    }
}
