namespace DefaultNamespace.Signals
{
    public interface IPauseListener
    {
        void OnPause(PauseSignal signal);
    }
}