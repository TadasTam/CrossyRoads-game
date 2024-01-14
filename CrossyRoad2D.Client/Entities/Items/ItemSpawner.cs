using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Entities.ItemsFactory;
using CrossyRoad2D.Client.Singletons;
using CrossyRoad2D.Common.Models;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.Items
{
    public class ItemSpawner : Entity
    {
        private double _lastSpawn = TimeState.Instance.TimeSecondsSinceStart;
        private const double SPAWN_INTERVAL_SECONDS = 5;
        private ItemCreator _creator;

        public ItemSpawner() : base(EntityType.ItemSpawner)
        {
            _creator = new ItemCreator();
        }

        public override void PrioritizedUpdate()
        {
            if (_lastSpawn + SPAWN_INTERVAL_SECONDS < TimeState.Instance.TimeSecondsSinceStart)
            {
                GenerateRandomItem();
                _lastSpawn = TimeState.Instance.TimeSecondsSinceStart;
            }
        }

        private void GenerateRandomItem()
        {
            List<string> myList = new List<string> { "Apple", "Potion", "Coin", "BadCoin", "Heart"};

            Random random = new Random();

            int randomIndex = random.Next(myList.Count);
            string itemType = myList[randomIndex];
            int x = random.Next(0, 20);
            int y = random.Next(0, 11);

            Entity newItem = _creator.factorySpawnItem(itemType, new Position(x, y));
            EntityCollection.Instance.AddEntity(newItem);
        }


        //int count = 0;
        //double initMemory = 0.0;
        //double finalMemory = 0.0;
        //string path = "C://Users//HP//Documents//GitHub//opp_crossy_road_2d//CrossyRoad2D.Client//Debug//newUI23.txt";

        //private void GenerateRandomItem()
        //{
        //    if (count == 1)
        //    {
        //        using (Process proc = Process.GetCurrentProcess())
        //        {
        //            initMemory = proc.PrivateMemorySize64 / (1024 * 1024);
        //        }
        //    }

        //    List<string> myList = new List<string> { "Apple", "Potion", "Coin", "BadCoin" };

        //    Random random = new Random();

        //    int randomIndex = random.Next(myList.Count);
        //    string itemType = myList[randomIndex];
        //    int x = random.Next(0, 20);
        //    int y = random.Next(0, 11);

        //    Entity newItem = _creator.factorySpawnItem(itemType, new Position(x, y));
        //    EntityCollection.Instance.AddEntity(newItem);


        //    if (count == 201)
        //    {
        //        using (Process proc = Process.GetCurrentProcess())
        //        {
        //            finalMemory = proc.PrivateMemorySize64 / (1024 * 1024);
        //        }

        //        using (StreamWriter writer = new StreamWriter(path))
        //        {
        //            writer.WriteLine(initMemory);
        //            writer.WriteLine(finalMemory - initMemory);
        //            writer.WriteLine(finalMemory);
        //        }
        //    }

        //    count++;
        //}


    }
}
