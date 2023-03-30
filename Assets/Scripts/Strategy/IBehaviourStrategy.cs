using System;

namespace DefaultNamespace.Strategy
{
    public interface IBehaviourStrategy
    {
        void PassiveBehaviour(EnemyView enemyView);
        void ActiveBehaviour(EnemyView enemyView);
        void TakeDamageBehaviour(EnemyView enemyView, int health);
    }
}