using System;
using System.Collections.Generic;
using DefaultNamespace.Strategy;
using UnityEngine;

namespace DefaultNamespace.Factories
{
    public class EnemyFactory
    {
        private readonly StrategiesFactory _strategiesFactory;
        // private readonly PassiveEnemyFactory _passiveEnemyFactory;
        // private readonly PeekOutEnemyFactory _peekOutEnemyFactory;
        // private readonly WalkingEnemyFactory _walkingEnemyFactory;
        private readonly List<IBehaviourStrategy> _strategies;

        public EnemyFactory(StrategiesFactory strategiesFactory)
        {
            _strategiesFactory = strategiesFactory;
            _strategies = _strategiesFactory.CreateStrategies();
        }

        public EnemyView CreateEnemy(EnemyData data)
        {
            var view = MonoBehaviour.Instantiate(data.Prefab, data.SpawnPosition.position, Quaternion.identity);
            var model = new EnemyModel(data);
            var controller = new EnemyController(_strategies, view, model);

            Debug.Log($"Создан враг {model.Type}, на позиции {view.transform.position}");
            return view;
        }

        // public EnemyView GetEnemy(EnemyData data)
        // {
        //     switch (data.Type)
        //     {
        //         case EEnemyType.PassiveEnemy:
        //             return _passiveEnemyFactory.CreateEnemy(data);
        //
        //         case EEnemyType.WalkingEnemy:
        //             return _walkingEnemyFactory.CreateEnemy(data);
        //
        //         case EEnemyType.PeekOutEnemy:
        //             return _peekOutEnemyFactory.CreateEnemy(data);
        //     }
        //     Debug.LogException(new Exception("в EnemyFactory.GetEnemy враг не создался!"));
        //     return null;
        // }
    }
}