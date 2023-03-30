using System.Collections.Generic;

namespace DefaultNamespace.Signals
{
    public class EnemyDamageSignalHandler
    {
        private readonly List<IEnemyDamageListener> _listeners;

        public EnemyDamageSignalHandler(List<IEnemyDamageListener> listeners)
        {
            _listeners = listeners;
        }

        public void AddListener(IEnemyDamageListener listener)
        {
            if (_listeners.Contains(listener)) return;
            _listeners.Add(listener);
        }

        public void RemoveListener(IEnemyDamageListener listener)
        {
            if (!_listeners.Contains(listener)) return;
            _listeners.Remove(listener);
        }

        public void Fire(EnemyDamageSignal signal)
        {
            for (var i = 0; i < _listeners.Count; i++)
            {
                _listeners[i].OnEnemyDamage(signal); 
            }
        }
    }
}