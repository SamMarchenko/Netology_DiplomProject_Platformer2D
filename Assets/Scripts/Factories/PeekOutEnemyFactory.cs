using DefaultNamespace.Player;

namespace DefaultNamespace.Factories
{
    public class PeekOutEnemyFactory : EnemyFactory
    {
        private EnemyView _enemyPrefab;
        public override IEnemy GetEnemy()
        {
            var enemy = new PeekOutEnemy(_enemyPrefab);
            return enemy;
        }
    }
}