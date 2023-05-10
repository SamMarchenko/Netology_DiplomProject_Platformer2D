using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

namespace DefaultNamespace.Strategy
{
    public class MovingEnemyStrategy : IBehaviourStrategy
    {
        private WalkingEnemyView _view;
        private Tween _tween;


        public void PassiveBehaviour(EnemyView enemyView)
        {
            SetView(enemyView);

            if (enemyView.HasTarget)
            {
                return;
            }

            _view.AttentionSprite.SetActive(false);
            _view.BehaviourAnimator.SetInteger("State", 1);
            _view.Rigidbody2D.velocity = Vector2.zero;

            _view.MoveDirection = _view.SpriteRenderer.flipX ? Vector2.left : Vector2.right;
        }


        public void ActiveBehaviour(EnemyView enemyView)
        {
            SetView(enemyView);

            if (!_view.HasTarget)
            {
                return;
            }

            _view.BehaviourAnimator.SetInteger("State", 1);
            _view.AttentionSprite.SetActive(true);
        }

        public void TakeDamageBehaviour(EnemyView enemyView, int health)
        {
            _tween.Kill();
            _view.TakeDamage();
            _tween = _view.transform.DOShakeScale(0.1f, _view.DamageShakeForce, 10, 5f, false)
                .OnComplete(() => CheckDeath(health));
        }

        private void CheckDeath(int health)
        {
            if (health == 0)
            {
                _view.IsDead = true;
                _view.SpriteRenderer.DOFade(0, 0.5f).OnComplete(() => _view.Dead());
            }
        }

        private void SetView(EnemyView enemyView)
        {
            if (_view == null || _view != enemyView)
            {
                _view = (WalkingEnemyView) enemyView;
                Subscribe();
            }
        }

        private void Subscribe()
        {
            _view.OnTheEdgePlatform += OnTheEdgePlatform;
            _view.OnConnectWithPlayer += OnConnectWithPlayer;
        }

        private void OnConnectWithPlayer(EUnitType unit)
        {
            _view.ExplodeSelf();
        }


        private void OnTheEdgePlatform()
        {
            _view.MoveDirection *= -1f;
            _view.Rigidbody2D.velocity = Vector2.zero;
        }
    }
}