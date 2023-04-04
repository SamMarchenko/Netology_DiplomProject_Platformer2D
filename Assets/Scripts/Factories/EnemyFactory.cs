using System.Collections.Generic;
using DefaultNamespace.Signals;
using DefaultNamespace.Strategy;
using UnityEngine;

namespace DefaultNamespace.Factories
{
    public class EnemyFactory
    {
        private readonly EnemyDamageSignalHandler _enemyDamageSignalHandler;
        private readonly StrategiesFactory _strategiesFactory;
        private readonly ProjectileFactory _projectileFactory;
        private readonly PlayerSignalBus _bus;
        private readonly List<IBehaviourStrategy> _strategies;

        public EnemyFactory(EnemyDamageSignalHandler enemyDamageSignalHandler,StrategiesFactory strategiesFactory, ProjectileFactory projectileFactory, PlayerSignalBus bus)
        {
            _enemyDamageSignalHandler = enemyDamageSignalHandler;
            _strategiesFactory = strategiesFactory;
            _projectileFactory = projectileFactory;
            _bus = bus;
            _strategies = _strategiesFactory.CreateStrategies();
        }

        public EnemyView CreateEnemy(EnemyData data)
        {
            var view = MonoBehaviour.Instantiate(data.Prefab, data.SpawnPosition.position, Quaternion.identity);
            var model = new EnemyModel(data);
            var controller = new EnemyController(_strategies, view, model, _projectileFactory, _bus, _enemyDamageSignalHandler);
            
            if (data.ContainLoot)
            {
                view.Loot = data.Loot;
                if(view.Loot == null) Debug.LogError("Ошибка! лут в enemyData не указан");
            }
            
            return view;
        }
    }
}