namespace Refactor.States
{
    public interface IState
    {
        public void InjectStateMachine(GameStateMachine gameStateMachine);
        public void Enter();
        public void Exit();
    }
}