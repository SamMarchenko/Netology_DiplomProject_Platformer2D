using System;
using System.Collections.Generic;
using DefaultNamespace.Strategy;
using UnityEngine;

namespace DefaultNamespace.Factories
{
    public class EnemyFactory
    {
        private readonly StrategiesFactory _strategiesFactory;
        private readonly ProjectileFactory _projectileFactory;
        private readonly List<IBehaviourStrategy> _strategies;

        public EnemyFactory(StrategiesFactory strategiesFactory, ProjectileFactory projectileFactory)
        {
            _strategiesFactory = strategiesFactory;
            _projectileFactory = projectileFactory;
            _strategies = _strategiesFactory.CreateStrategies();
        }

        public EnemyView CreateEnemy(EnemyData data)
        {
            var view = MonoBehaviour.Instantiate(data.Prefab, data.SpawnPosition.position, Quaternion.identity);
            var model = new EnemyModel(data);
            var controller = new EnemyController(_strategies, view, model, _projectileFactory);

            
            return view;
        }
    }
}