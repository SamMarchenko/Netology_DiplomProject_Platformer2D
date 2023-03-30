using System;
using DefaultNamespace.Factories;
using DG.Tweening;
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

        public void TakeDamageBehaviour(EnemyView enemyView, int health)
        {
            _view.transform.DOShakeScale(0.1f, _view.DamageShakeForce, 10, 5f, false)
                .OnComplete(() => CheckDeath(health));
        }

        private void CheckDeath(int health)
        {
            if (health == 0)
            {
                _view.IsDead = true;
                _view.SpriteRenderer.DOFade(0, 1f).OnComplete(() => _view.Dead());
            }
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