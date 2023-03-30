using UnityEngine;

namespace DefaultNamespace.Strategy
{
    public class PassiveEnemyStrategy : IBehaviourStrategy
    {
        private PassiveEnemyView _view;

        public void PassiveBehaviour(EnemyView enemyView)
        {
           SetView(enemyView);
        }

        public void ActiveBehaviour(EnemyView enemyView)
        {
            SetView(enemyView);
        }

        public void TakeDamageBehaviour(EnemyView enemyView, int health)
        {
            //бессмертный
        }


        private void SetView(EnemyView enemyView)
        {
            if (_view == null || _view != enemyView)
            {
                _view = (PassiveEnemyView) enemyView;
                
            }
        }
        
    }
}