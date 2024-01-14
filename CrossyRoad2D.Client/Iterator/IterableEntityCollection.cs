using CrossyRoad2D.Client.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Iterator
{
    internal interface IterableEntityCollection
    {
        public IIterator createIterator(EntityType type);
        public IIterator createLethalEntityIterator();
        public IIterator createUnpassableEntityIterator();
        public IIterator createTruckCollidableEntityIterator();
    }
}
