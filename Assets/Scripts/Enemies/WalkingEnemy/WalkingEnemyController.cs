using DefaultNamespace.Players;

namespace DefaultNamespace
{
    public class WalkingEnemyController
    {
        private readonly WalkingEnemyModel _enemyModel;
        private readonly WalkingEnemyView _enemyView;
        private readonly AnimationController _animationController;

        public WalkingEnemyController(WalkingEnemyModel enemyModel, WalkingEnemyView enemyView)
        {
            _enemyModel = enemyModel;
            _enemyView = enemyView;
            _animationController = new AnimationController(_enemyView.Animator);
            _animationController.PlayAnimation(EAnimStates.Run);
        }
    }
}