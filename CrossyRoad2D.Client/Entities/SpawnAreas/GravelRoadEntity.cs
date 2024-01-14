using CrossyRoad2D.Client.Components.Xaml;
using CrossyRoad2D.Client.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrossyRoad2D.Client.Entities.SpawnAreas
{
    public class GravelRoadEntity : SpawnArea
    {
        private bool _fromLeft;

        public GravelRoadEntity(int positionY, List<SpawnableWithChance> spawnables) : base(new GravelRoadXamlComponent(), positionY,
            minSpawnInterval: 0.5,
            maxSpawnInterval: 1.0,
            spawnables)
        {
            _fromLeft = new Random().NextDouble() > 0.5;
        }

        public override void Spawn()
        {
            var positionY = _positionY + 0.5;
            EntityCollection.Instance.AddEntity(GetRandomSpawnable(positionY, _fromLeft));
        }
    }
}
