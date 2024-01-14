using CrossyRoad2D.Client.Entities.Common;
using CrossyRoad2D.Client.Singletons;
using CrossyRoad2D.Common.Enums;
using CrossyRoad2D.Common.MessageContents;
using CrossyRoad2D.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using CrossyRoad2D.Client.Components.Xaml;
using CrossyRoad2D.Common.Utils;
using CrossyRoad2D.Common.Models;
using System.Windows.Navigation;

namespace CrossyRoad2D.Client.Entities.SpawnAreas
{
    public abstract class SpawnArea : Entity
    {
        private XamlComponent _doubleHeightComponent;
        private double _minSpawnInterval;
        private double _maxSpawnInterval;

        protected int _positionY;

        private double _spawnTime;

        private List<SpawnableWithChance> _spawnables;

        protected SpawnArea(XamlComponent doubleHeightComponent,
            int positionY, double minSpawnInterval, double maxSpawnInterval,
            List<SpawnableWithChance> spawnables) : base(EntityType.SpawnArea)
        {
            _doubleHeightComponent = doubleHeightComponent;
            _positionY = positionY;
            _minSpawnInterval = minSpawnInterval;
            _maxSpawnInterval = maxSpawnInterval;
            _spawnables = spawnables;
            _spawnTime = TimeState.Instance.TimeSecondsSinceStart + _minSpawnInterval;

            RenderPriority = EntityRenderPriority.SpawnArea;
            UpdatePriority = EntityUpdatePriority.SpawnArea;
        }

        public override void Render(Canvas canvas)
        {
            for (int currentTile = 0; currentTile < 1; currentTile++)
            {
                var currentRectangle = new Rectangle(currentTile, _positionY, GridUtils.TileCountX, 2);
                _doubleHeightComponent.Rectangle = currentRectangle;
                _doubleHeightComponent.Render(canvas);
            }
        }

        public override void PrioritizedUpdate()
        {
            if (_spawnTime < TimeState.Instance.TimeSecondsSinceStart)
            {
                Random random = new Random();
                var interval = random.NextDouble() * (_maxSpawnInterval - _minSpawnInterval) + _minSpawnInterval;
                _spawnTime = TimeState.Instance.TimeSecondsSinceStart + interval;
                Spawn();
            }
        }

        public abstract void Spawn();

        protected Spawnable GetRandomSpawnable(double positionY, bool fromLeft)
        {
            var totalChance = _spawnables.Sum(s => s.Chance);
            var randomDouble = new Random().NextDouble();

            var currentChance = 0.0;
            foreach(var spawnable in _spawnables)
            {
                currentChance += spawnable.Chance;
                if(currentChance / totalChance > randomDouble)
                {
                    return spawnable.GetSpawnable(positionY, fromLeft);
                }
            }

            return _spawnables.ElementAtOrDefault(_spawnables.Count - 1)?.GetSpawnable(positionY, fromLeft);
        }
    }

    public class SpawnableWithChance
    {
        public Func<double, bool, Spawnable> GetSpawnable { get; set; }
        public double Chance { get; set; }
    }
}
