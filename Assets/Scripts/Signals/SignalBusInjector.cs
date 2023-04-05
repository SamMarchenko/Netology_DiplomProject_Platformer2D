using Zenject;

namespace DefaultNamespace.Signals
{
    public class SignalBusInjector: IInitializable
    {
        private readonly PlayerSignalBus _playerSignalBus;
        private readonly PlayerDamageSignalHandler _playerDamageSignalHandler;
        private readonly PlayerHealSignalHandler _playerHealSignalHandler;
        private readonly EnemySignalBus _enemySignalBus;
        private readonly EnemyDamageSignalHandler _enemyDamageSignalHandler;

        public SignalBusInjector(PlayerSignalBus playerSignalBus,
            PlayerDamageSignalHandler playerDamageSignalHandler,
            PlayerHealSignalHandler playerHealSignalHandler,
            EnemySignalBus enemySignalBus,
            EnemyDamageSignalHandler enemyDamageSignalHandler)
        {
            _playerSignalBus = playerSignalBus;
            _playerDamageSignalHandler = playerDamageSignalHandler;
            _playerHealSignalHandler = playerHealSignalHandler;
            _enemySignalBus = enemySignalBus;
            _enemyDamageSignalHandler = enemyDamageSignalHandler;
        }
        
        public void Initialize()
        {
            _playerSignalBus.Init(_playerDamageSignalHandler, _playerHealSignalHandler);
            _enemySignalBus.Init(_enemyDamageSignalHandler);
        }
    }
}