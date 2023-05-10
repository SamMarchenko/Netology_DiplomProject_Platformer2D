namespace Refactor.States
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
}