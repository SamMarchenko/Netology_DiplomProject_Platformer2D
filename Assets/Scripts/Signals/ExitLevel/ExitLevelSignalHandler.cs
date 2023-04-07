using System.Collections.Generic;

namespace DefaultNamespace.Signals
{
    public class ExitLevelSignalHandler
    {
        private readonly List<IExitLevelListener> _listeners;

        public ExitLevelSignalHandler(List<IExitLevelListener> listeners)
        {
            _listeners = listeners;
        }

        public void Fire(ExitLevelSignal signal)
        {
            foreach (var listener in _listeners)
            {
                listener.OnExitLevel(signal);
            }
        }
    }
}