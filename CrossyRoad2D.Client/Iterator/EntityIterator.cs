using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Iterator
{
    internal class EntityIterator : IIterator
    {
        private List<Entity> Collection;
        private EntityType Type;

        public EntityIterator(List<Entity> collection, EntityType type)
        {
            Collection = collection;
            Type = type;
        }

        private int position = 0;

        public Entity getNext()
        {
            Entity e = Collection[position];
            position++;
            return e;
        }

        public bool hasMore()
        {
            while (position < Collection.Count)
            {
                Entity e = Collection[position];
                if (e.Type.Equals(Type))
                    return true;
                else
                    position++;
            }
            return false;
        }
    }
}
