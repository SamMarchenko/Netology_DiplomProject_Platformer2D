using System;
using UnityEngine;

namespace DefaultNamespace.Factories
{
    public class EnemyFactory
    {
        private readonly PassiveEnemyFactory _passiveEnemyFactory;
        private readonly PeekOutEnemyFactory _peekOutEnemyFactory;
        private readonly WalkingEnemyFactory _walkingEnemyFactory;

        public EnemyFactory(PassiveEnemyFactory passiveEnemyFactory, PeekOutEnemyFactory peekOutEnemyFactory,
            WalkingEnemyFactory walkingEnemyFactory)
        {
            _passiveEnemyFactory = passiveEnemyFactory;
            _peekOutEnemyFactory = peekOutEnemyFactory;
            _walkingEnemyFactory = walkingEnemyFactory;
        }

        public EnemyView GetEnemy(EnemyData data)
        {
            switch (data.Type)
            {
                case EEnemyType.PassiveEnemy:
                    return _passiveEnemyFactory.CreateEnemy(data);

                case EEnemyType.WalkingEnemy:
                    return _walkingEnemyFactory.CreateEnemy(data);

                case EEnemyType.PeekOutEnemy:
                    return _peekOutEnemyFactory.CreateEnemy(data);
            }
            Debug.LogException(new Exception("в EnemyFactory.GetEnemy враг не создался!"));
            return null;
        }
    }
}