namespace DefaultNamespace.Signals
{
    public interface IInventaryUpdateListener
    {
        void OnInventaryUpdate(InventarySignal signal);
    }
}