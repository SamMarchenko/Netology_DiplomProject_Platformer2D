using UnityEngine;

namespace DefaultNamespace.Strategy
{
    public class PeekOutEnemyStrategy : IBehaviourStrategy
    {
        public void PassiveBehaviour(EnemyView enemyView)
        {
            Debug.Log("Поворачиваюсь по сторонам в ожидании игрока");
        }

        public void ActiveBehaviour(EnemyView enemyView)
        {
            Debug.Log("Повернут в направлении игрока и атакую его снарядом");
        }
    }
}