using UnityEngine;
using Zenject;

namespace Refactor.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameStateMachine _gameStateMachine;

        public override void InstallBindings()
        {
            var gameStateMachine = Container.InstantiatePrefabForComponent<GameStateMachine>(_gameStateMachine);
            Container.BindInterfacesAndSelfTo<GameStateMachine>().FromInstance(gameStateMachine).AsSingle().NonLazy();
        }
    }
}