using System.Collections.Generic;

namespace DefaultNamespace.Signals
{
    public class PauseSignalHandler
    {
        private readonly List<IPauseListener> _listeners;

        public PauseSignalHandler(List<IPauseListener> listeners)
        {
            _listeners = listeners;
        }

        public void Fire(PauseSignal signal)
        {
            foreach (var listener in _listeners)
            {
                listener.OnPause(signal);
            }
        }
    }
}