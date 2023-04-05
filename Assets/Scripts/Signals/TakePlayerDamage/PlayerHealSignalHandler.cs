using System.Collections.Generic;

namespace DefaultNamespace.Signals
{
    public class PlayerHealSignalHandler
    {
        private readonly List<IPlayerHealListener> _listeners;

        public PlayerHealSignalHandler(List<IPlayerHealListener> listeners)
        {
            _listeners = listeners;
        }

        public void Fire(PlayerHealSignal signal)
        {
            foreach (var listener in _listeners)
            {
                listener.OnPlayerHeal(signal);
            }
        }
    }
}