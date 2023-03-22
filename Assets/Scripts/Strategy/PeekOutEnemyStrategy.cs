using System;
using DefaultNamespace.Factories;
using UnityEngine;

namespace DefaultNamespace.Strategy
{
    public class PeekOutEnemyStrategy : IBehaviourStrategy
    {
        private PeekOutEnemyView _view;



        public void PassiveBehaviour(EnemyView enemyView)
        {
            SetView(enemyView);

            if (_view.HasTarget)
            {
                return;
            }

            _view.Animator.SetInteger("State", 0);
            _view.AttentionSprite.SetActive(false);
        }

        public void ActiveBehaviour(EnemyView enemyView)
        {
            SetView(enemyView);
            // отключает аниматор
            _view.Animator.SetInteger("State", Int32.MaxValue);
            _view.AttentionSprite.SetActive(true);
        }


        private void SetView(EnemyView enemyView)
        {
            if (_view == null || _view != enemyView)
            {
                _view = (PeekOutEnemyView) enemyView;
            }
        }
    }
}