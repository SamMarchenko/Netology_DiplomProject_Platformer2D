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

            _view.Animator.SetInteger("State", 0);
            _view.MoveDirection = Vector2.left;
            Debug.Log("патрулирую платформу");
        }


        public void ActiveBehaviour(EnemyView enemyView)
        {
            SetView(enemyView);

            if (!_view.HasTarget)
            {
                return;
            }

            _view.Animator.SetInteger("State", 1);
            _view.MoveDirection = _view.Target.transform.position - _view.transform.position;
            Debug.Log("Преследую игрока на моей платформе для атаки");
        }

        private void SetView(EnemyView enemyView)
        {
            if (_view == null || _view != enemyView)
            {
                _view = (WalkingEnemyView) enemyView;
                _view.OnTheEdgePlatform += OnTheEdgePlatform;
            }
        }


        private void OnTheEdgePlatform()
        {
            _view.MoveDirection *= -1f;
        }
    }
}