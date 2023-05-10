namespace Refactor.States
{
    public interface IExitableState
    {
        public void InjectStateMachine(GameStateMachine gameStateMachine);
        public void Exit(); 
    }
}