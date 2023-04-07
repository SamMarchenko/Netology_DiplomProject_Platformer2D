namespace DefaultNamespace.Signals
{
    public class InventaryUpdateSignalBus
    {
        private InventaryUpdateSignalHandler _inventaryUpdateSignalHandler;

        public void Init(InventaryUpdateSignalHandler inventaryUpdateSignalHandler)
        {
            _inventaryUpdateSignalHandler = inventaryUpdateSignalHandler;
        }

        public void InventaryUpdate(InventarySignal signal)
        {
            _inventaryUpdateSignalHandler.Fire(signal);
        }
    }
}