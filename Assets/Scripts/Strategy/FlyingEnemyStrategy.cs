using DefaultNamespace.FlyingEnemy;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Strategy
{
    public class FlyingEnemyStrategy : IBehaviourStrategy
    {
        private FlyingEnemyView _view;
        private Tween _tween;
        
        public void PassiveBehaviour(EnemyView enemyView)
        {
            SetView(enemyView);

            if (enemyView.HasTarget)
            {
                return;
            }
        }

        public void ActiveBehaviour(EnemyView enemyView)
        {
            SetView(enemyView);

            if (!_view.HasTarget)
            {
                return;
            }
            
            _view.AttentionSprite.SetActive(true);
        }

        public void TakeDamageBehaviour(EnemyView enemyView, int health)
        {
            _tween.Kill();
            _tween = _view.transform.DOShakeScale(0.1f, _view.DamageShakeForce, 10, 5f, false)
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
                _view = (FlyingEnemyView) enemyView;
                Subscribe();
            }
        }
        
        private void Subscribe()
        {
            _view.OnFarFromPlatform += OnFarFromPlatform;
            _view.OnTheEdgePlatform += ReturnOnPlatform;
        }

        private void ReturnOnPlatform()
        {
            if (_view.IsNeedBack)
            {
                _view.IsNeedBack = false;
            }
        }
        

        private void OnFarFromPlatform()
        {
            _view.Target = null;
            _view.IsNeedBack = true;
        }
    }
}