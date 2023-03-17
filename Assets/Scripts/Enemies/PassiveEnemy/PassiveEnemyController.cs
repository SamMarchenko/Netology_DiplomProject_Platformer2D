namespace DefaultNamespace
{
    public class PassiveEnemyController
    {
        private readonly PassiveEnemyModel _passiveEnemyModel;
        private readonly PassiveEnemyView _passiveEnemyView;

        public PassiveEnemyController(PassiveEnemyModel passiveEnemyModel, PassiveEnemyView passiveEnemyView)
        {
            _passiveEnemyModel = passiveEnemyModel;
            _passiveEnemyView = passiveEnemyView;
        }
    }
}