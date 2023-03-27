using Zenject;

namespace DefaultNamespace.Signals
{
    public class SignalBusInjector: IInitializable
    {
        private readonly PlayerSignalBus _playerSignalBus;
        private readonly PlayerDamageSignalHandler _playerDamageSignalHandler;

        public SignalBusInjector(PlayerSignalBus playerSignalBus, PlayerDamageSignalHandler playerDamageSignalHandler)
        {
            _playerSignalBus = playerSignalBus;
            _playerDamageSignalHandler = playerDamageSignalHandler;
        }
        
        public void Initialize()
        {
            _playerSignalBus.Init(_playerDamageSignalHandler);
        }
    }
}