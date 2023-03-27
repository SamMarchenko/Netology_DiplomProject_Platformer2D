namespace DefaultNamespace.Signals
{
    public class PlayerSignalBus
    {
        private PlayerDamageSignalHandler _playerDamageSignalHandler;

        public void Init(PlayerDamageSignalHandler playerDamageSignalHandler)
        {
            _playerDamageSignalHandler = playerDamageSignalHandler;
        }

        public void PlayerTakeDamage(PlayerDamageSignal signal)
        {
            _playerDamageSignalHandler.Fire(signal);
        }
    }
}