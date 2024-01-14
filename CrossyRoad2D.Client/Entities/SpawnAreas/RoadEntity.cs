using CrossyRoad2D.Client.Components.Xaml;
using CrossyRoad2D.Client.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.SpawnAreas
{
    public class RoadEntity : SpawnArea
    {
        public RoadEntity(int positionY, List<SpawnableWithChance> spawnables) : base(new RoadXamlComponent(), positionY,
            minSpawnInterval: 0.25,
            maxSpawnInterval: 1,
            spawnables)
        {
        }

        public override void Spawn()
        {
            var random = new Random();
            var fromLeft = random.NextDouble() > 0.5;
            var positionY = fromLeft ? _positionY : _positionY + 1;

            EntityCollection.Instance.AddEntity(GetRandomSpawnable(positionY, fromLeft));
        }
    }
}
