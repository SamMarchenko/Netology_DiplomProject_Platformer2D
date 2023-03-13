using DefaultNamespace.Player;

namespace DefaultNamespace.Factories
{
    public abstract class EnemyFactory
    {
        public EnemyView EnemyView { get; set; }
        
        public abstract IEnemy GetEnemy();
    }
}