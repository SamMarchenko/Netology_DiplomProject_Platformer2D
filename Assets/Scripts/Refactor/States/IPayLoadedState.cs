namespace Refactor.States
{
    public interface IPayLoadedState<TPayLoad> : IExitableState
    {
        void Enter(TPayLoad payLoad);
    }
}