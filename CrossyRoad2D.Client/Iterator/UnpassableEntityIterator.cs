﻿using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Entities.Obstacles;
using CrossyRoad2D.Client.Entities.SpawnAreas;
using CrossyRoad2D.Client.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Iterator
{
    internal class UnpassableEntityIterator : IIterator
    {
        private List<Entity> Collection;

        public UnpassableEntityIterator(List<Entity> collection)
        {
            Collection = collection;
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
                IUnpassable entity = Collection[position] as IUnpassable;
                if (entity is not null)
                    return true;
                else
                    position++;
            }
            return false;
        }
    }
}
