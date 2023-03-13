using DefaultNamespace.Player;

namespace DefaultNamespace
{
    public class PeekOutEnemy : IEnemy
    {
        private readonly EnemyView _enemyView;
        public EEnemyType EEnemyType { get; }

        public PeekOutEnemy(EnemyView enemyView)
        {
            _enemyView = enemyView;
        }
    }
}