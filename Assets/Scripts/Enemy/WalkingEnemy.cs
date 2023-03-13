using DefaultNamespace.Player;

namespace DefaultNamespace
{
    public class WalkingEnemy : IEnemy
    {
        private readonly EnemyView _enemyView;
        public EnemyType EnemyType { get; }
        
        public WalkingEnemy(EnemyView enemyView)
        {
            _enemyView = enemyView;
        }
       
    }
}