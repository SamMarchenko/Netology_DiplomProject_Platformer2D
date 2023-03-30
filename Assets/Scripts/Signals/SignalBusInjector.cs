using Zenject;

namespace DefaultNamespace.Signals
{
    public class SignalBusInjector: IInitializable
    {
        private readonly PlayerSignalBus _playerSignalBus;
        private readonly PlayerDamageSignalHandler _playerDamageSignalHandler;
        private readonly EnemySignalBus _enemySignalBus;
        private readonly EnemyDamageSignalHandler _enemyDamageSignalHandler;

        public SignalBusInjector(PlayerSignalBus playerSignalBus, PlayerDamageSignalHandler playerDamageSignalHandler,
            EnemySignalBus enemySignalBus, EnemyDamageSignalHandler enemyDamageSignalHandler)
        {
            _playerSignalBus = playerSignalBus;
            _playerDamageSignalHandler = playerDamageSignalHandler;
            _enemySignalBus = enemySignalBus;
            _enemyDamageSignalHandler = enemyDamageSignalHandler;
        }
        
        public void Initialize()
        {
            _playerSignalBus.Init(_playerDamageSignalHandler);
            _enemySignalBus.Init(_enemyDamageSignalHandler);
        }
    }
}