using UnityEngine;

namespace DefaultNamespace.Strategy
{
    public class MovingEnemyStrategy : IBehaviourStrategy
    {
        private WalkingEnemyView _view;


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
            
            Debug.Log("патрулирую платформу");
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
            Debug.Log("Преследую игрока на моей платформе для атаки");
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

        private void OnConnectWithPlayer()
        {
          _view.ExplodeSelf();
        }


        private void OnTheEdgePlatform()
        {
            Debug.Log("Разворот врага");
            _view.MoveDirection *= -1f;
            _view.Rigidbody2D.velocity = Vector2.zero;
        }
    }
}