using DefaultNamespace.Player;

namespace DefaultNamespace.Factories
{
    public class WalkingEnemyFactory : EnemyFactory
    {
        private EnemyView _enemyPrefab;
        public override IEnemy GetEnemy()
        {
            var enemy = new WalkingEnemy(_enemyPrefab);
            return enemy;
        }
    }
}