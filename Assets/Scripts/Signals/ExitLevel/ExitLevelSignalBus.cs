namespace DefaultNamespace.Signals
{
    public class ExitLevelSignalBus
    {
        private ExitLevelSignalHandler _exitLevelSignalHandler;
        public void Init(ExitLevelSignalHandler exitLevelSignalHandler)
        {
            _exitLevelSignalHandler = exitLevelSignalHandler;
        }

        public void ExitLevel(ExitLevelSignal signal)
        {
            _exitLevelSignalHandler.Fire(signal);
        }
    }
}