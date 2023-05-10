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
            _mainMenuService.OnLoadNewGame += OnLoadNewGame;
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
        
        private void OnLoadNewGame()
        {
            //todo: захардкожен 1 уровень. Переделать
            _gameStateMachine.Enter<LoadLevelState, string>("Level1");
        }
    }
}