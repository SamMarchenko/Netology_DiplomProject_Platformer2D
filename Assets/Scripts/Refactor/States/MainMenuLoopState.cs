using UnityEngine;

namespace Refactor.States
{
    public class MainMenuLoopState : IState
    {
        private readonly MainMenuService _mainMenuService;
        private GameStateMachine _gameStateMachine;

        public MainMenuLoopState(MainMenuService mainMenuService)
        {
            _mainMenuService = MonoBehaviour.Instantiate(mainMenuService);
        }


        public void InjectStateMachine(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            _mainMenuService.OpenMainMenu();
        }

        public void Exit()
        {
        }
    }
}