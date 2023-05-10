using Refactor.States;
using UnityEngine;
using Zenject;

namespace Refactor.Installers
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuService _menuUIPrefab;
        [SerializeField] private GameStateMachine _gameStateMachine;
        public override void InstallBindings()
        {
            Container.BindInstance(_menuUIPrefab);
            Container.BindInterfacesAndSelfTo<MainMenuLoopState>().AsSingle().NonLazy();
            
            var gameStateMachine = Container.InstantiatePrefabForComponent<GameStateMachine>(_gameStateMachine);
            Container.BindInterfacesAndSelfTo<GameStateMachine>().FromInstance(gameStateMachine).AsSingle().NonLazy();
        }
    }
}