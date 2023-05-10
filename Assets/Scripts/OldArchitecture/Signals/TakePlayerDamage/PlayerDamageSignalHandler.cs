using System.Collections.Generic;

namespace DefaultNamespace.Signals
{
    public class PlayerDamageSignalHandler
    {
        private readonly List<IPlayerDamageListener> _listeners;

        public PlayerDamageSignalHandler(List<IPlayerDamageListener> listeners)
        {
            _listeners = listeners;
        }

        public void Fire(PlayerDamageSignal signal)
        {
            foreach (var listener in _listeners)
            {
                listener.OnPlayerDamage(signal);
            }
        }
    }
}