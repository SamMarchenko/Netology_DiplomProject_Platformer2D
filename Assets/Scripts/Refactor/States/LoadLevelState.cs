using UnityEngine;

namespace Refactor.States
{
    public class LoadLevelState : IPayLoadedState<string>
    {
        private readonly SceneLoadingService _sceneLoadingService;
        private readonly LoadingCurtain _curtain;
        private GameStateMachine _gameStateMachine;

        public LoadLevelState(SceneLoadingService sceneLoadingService, LoadingCurtain curtain)
        {
            _sceneLoadingService = sceneLoadingService;
            _curtain = MonoBehaviour.Instantiate(curtain);
        }

        public void InjectStateMachine(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter(string sceneName)
        {
            _sceneLoadingService.Init(_curtain, _gameStateMachine);
            _curtain.Show();
            _sceneLoadingService.Load(sceneName, Exit);
        }

        public void Exit()
        {
            _curtain.Hide();
        }
    }
}