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
        private readonly PauseSignalBus _pauseSignalBus;
        private readonly PauseSignalHandler _pauseSignalHandler;

        public SignalBusInjector(PlayerSignalBus playerSignalBus,
            PlayerDamageSignalHandler playerDamageSignalHandler,
            PlayerHealSignalHandler playerHealSignalHandler,
            EnemySignalBus enemySignalBus,
            EnemyDamageSignalHandler enemyDamageSignalHandler,
            PauseSignalBus pauseSignalBus,
            PauseSignalHandler pauseSignalHandler)
        {
            _playerSignalBus = playerSignalBus;
            _playerDamageSignalHandler = playerDamageSignalHandler;
            _playerHealSignalHandler = playerHealSignalHandler;
            
            _enemySignalBus = enemySignalBus;
            _enemyDamageSignalHandler = enemyDamageSignalHandler;
            
            _pauseSignalBus = pauseSignalBus;
            _pauseSignalHandler = pauseSignalHandler;
        }
        
        public void Initialize()
        {
            _playerSignalBus.Init(_playerDamageSignalHandler, _playerHealSignalHandler);
            _enemySignalBus.Init(_enemyDamageSignalHandler);
            _pauseSignalBus.Init(_pauseSignalHandler);
        }
    }
}