using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.Items
{
    public abstract class Item : Entity
    {
        public Rectangle Rectangle { get; set; } = new Rectangle();

        public Item(EntityType entityType) : base(entityType)
        {

        }

        public abstract void Consume(PlayerEntity player);
    }
}
