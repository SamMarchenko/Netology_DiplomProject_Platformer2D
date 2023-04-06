namespace DefaultNamespace.Signals
{
    public class PauseSignal
    {
        public readonly bool IsPause;

        public PauseSignal(bool isPause)
        {
            IsPause = isPause;
        }
    }
}