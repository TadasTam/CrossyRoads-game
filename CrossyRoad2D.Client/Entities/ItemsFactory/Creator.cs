using CrossyRoad2D.Client.Entities.Items;
using CrossyRoad2D.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Item = CrossyRoad2D.Client.Entities.Items.Item;

namespace CrossyRoad2D.Client.Entities.ItemsFactory
{
    public abstract class Creator
    {
        public abstract Item factorySpawnItem(string userInput, Position position);
    }
}
