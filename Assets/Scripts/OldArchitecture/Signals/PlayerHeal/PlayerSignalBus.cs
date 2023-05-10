namespace DefaultNamespace.Signals
{
    public class PlayerSignalBus
    {
        private PlayerDamageSignalHandler _playerDamageSignalHandler;
        private PlayerHealSignalHandler _playerHealSignalHandler;

        public void Init(PlayerDamageSignalHandler playerDamageSignalHandler, PlayerHealSignalHandler playerHealSignalHandler)
        {
            _playerDamageSignalHandler = playerDamageSignalHandler;
            _playerHealSignalHandler = playerHealSignalHandler;
        }

        public void PlayerTakeDamage(PlayerDamageSignal signal)
        {
            _playerDamageSignalHandler.Fire(signal);
        }

        public void PlayerHeal(PlayerHealSignal signal)
        {
            _playerHealSignalHandler.Fire(signal);
        }
    }
}