using System.Collections.Generic;

namespace DefaultNamespace.Signals
{
    public class InventaryUpdateSignalHandler
    {
        private readonly List<IInventaryUpdateListener> _listeners;

        public InventaryUpdateSignalHandler(List<IInventaryUpdateListener> listeners)
        {
            _listeners = listeners;
        }

        public void Fire(InventarySignal signal)
        {
            foreach (var listener in _listeners)
            {
                listener.OnInventaryUpdate(signal);
            }
        }
    }
}