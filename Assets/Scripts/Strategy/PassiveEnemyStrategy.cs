using UnityEngine;

namespace DefaultNamespace.Strategy
{
    public class PassiveEnemyStrategy : IBehaviourStrategy
    {
        public void PassiveBehaviour(EnemyView enemyView)
        {
            Debug.Log("Пассивная деревяха");
        }

        public void ActiveBehaviour(EnemyView enemyView)
        {
            Debug.Log("Активная деревяха");
        }
    }
}