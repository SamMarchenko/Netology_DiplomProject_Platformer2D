namespace DefaultNamespace.Signals
{
    public class PauseSignalBus
    {
        private PauseSignalHandler _pauseSignalHandler;
        public void Init(PauseSignalHandler pauseSignalHandler)
        {
            _pauseSignalHandler = pauseSignalHandler;
        }

        public void Pause(PauseSignal signal)
        {
            _pauseSignalHandler.Fire(signal);
        }
    }
}