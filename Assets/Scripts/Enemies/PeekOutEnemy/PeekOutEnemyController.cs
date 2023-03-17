using DefaultNamespace.Players;

namespace DefaultNamespace
{
    public class PeekOutEnemyController
    {
        private readonly PeekOutEnemyModel _enemyModel;
        private readonly PeekOutEnemyView _enemyView;
        private readonly AnimationController _animationController;

        public PeekOutEnemyController(PeekOutEnemyModel enemyModel, PeekOutEnemyView enemyView)
        {
            _enemyModel = enemyModel;
            _enemyView = enemyView;
            _animationController = new AnimationController(_enemyView.Animator);
            _animationController.PlayAnimation(EAnimStates.Idle);
        }
    }
}