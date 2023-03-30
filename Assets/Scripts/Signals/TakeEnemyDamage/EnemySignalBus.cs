namespace DefaultNamespace.Signals
{
    public class EnemySignalBus
    {
        private EnemyDamageSignalHandler _enemyDamageSignalHandler;

        public void Init(EnemyDamageSignalHandler enemyDamageSignalHandler)
        {
            _enemyDamageSignalHandler = enemyDamageSignalHandler;
        }

        public void EnemyTakeDamage(EnemyDamageSignal signal)
        {
            _enemyDamageSignalHandler.Fire(signal);
        }
    }
}