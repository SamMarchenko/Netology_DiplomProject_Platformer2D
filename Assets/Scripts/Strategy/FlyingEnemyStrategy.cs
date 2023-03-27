using DefaultNamespace.FlyingEnemy;

namespace DefaultNamespace.Strategy
{
    public class FlyingEnemyStrategy : IBehaviourStrategy
    {
        private FlyingEnemyView _view;
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
            if (_view.isNeedBack)
            {
                _view.isNeedBack = false;
            }
        }
        

        private void OnFarFromPlatform()
        {
            _view.Target = null;
            _view.isNeedBack = true;
        }
    }
}