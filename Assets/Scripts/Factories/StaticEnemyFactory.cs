using DefaultNamespace.Player;
using UnityEngine;

namespace DefaultNamespace.Factories
{
    public class StaticEnemyFactory : EnemyFactory
    {
        private EnemyView _enemyPrefab;
        public override IEnemy GetEnemy()
        {
            var enemy = new StaticEnemy(_enemyPrefab);
            return enemy;
        }
    }
}