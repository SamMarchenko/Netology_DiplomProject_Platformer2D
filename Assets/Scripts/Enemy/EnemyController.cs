namespace DefaultNamespace
{
    public class EnemyController
    {
        private readonly EnemyModel _enemyModel;
        private readonly EnemyView _enemyView;

        public EnemyController(EnemyModel enemyModel, EnemyView enemyView)
        {
            _enemyModel = enemyModel;
            _enemyView = enemyView;
        }
    }
}