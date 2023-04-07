using DefaultNamespace;
using DefaultNamespace.Doors;
using DefaultNamespace.Factories;
using DefaultNamespace.Players;
using DefaultNamespace.Players.MVC;
using DefaultNamespace.Signals;
using DefaultNamespace.UI;
using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    public SpawnPositions SpawnPositions;
    public PlayerView PlayerPrefab;
    public DoorView DoorPrefab;
    public UITopPanelCoreManager uiTopPanelCoreManagerPrefab;
    [SerializeField] private PauseWindowManager _pauseWindowManager;
    


    public override void InstallBindings()
    
    {
        Container.BindInstance(_pauseWindowManager);
        Container.BindInstance(SpawnPositions);
        BindFactories();
        BindEnemies();
        BindPlayer();
        BindDoor();
        BindSignals();
        Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle().NonLazy();
        BindInputSystems();
        BindUI();
    }
    
    private void BindSignals()
    {
        Container.BindInterfacesAndSelfTo<PlayerDamageSignalHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerHealSignalHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerSignalBus>().AsSingle().NonLazy();
        
        Container.BindInterfacesAndSelfTo<EnemyDamageSignalHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<EnemySignalBus>().AsSingle().NonLazy();
        
        Container.BindInterfacesAndSelfTo<PauseSignalHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PauseSignalBus>().AsSingle().NonLazy();
        
        Container.BindInterfacesAndSelfTo<ExitLevelSignalHandler>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<ExitLevelSignalBus>().AsSingle().NonLazy();
        
        
        Container.BindInterfacesAndSelfTo<SignalBusInjector>().AsSingle().NonLazy();
    }

    private void BindUI()
    {
        var healthBar = Container.InstantiatePrefabForComponent<UITopPanelCoreManager>(uiTopPanelCoreManagerPrefab);
        Container.BindInterfacesAndSelfTo<UITopPanelCoreManager>().FromInstance(healthBar).AsSingle().NonLazy();
    }
    

    private void BindFactories()
    {
        Container.BindInterfacesAndSelfTo<ProjectileFactory>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<StrategiesFactory>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle().NonLazy();
    }

    
    private void BindPlayer()
    {
        var playerView = Container.InstantiatePrefabForComponent<PlayerView>(PlayerPrefab,
            SpawnPositions.PlayerSpawnPos.position, Quaternion.identity, null);
        
        Container.Bind<PlayerView>().FromInstance(playerView).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle().NonLazy();
    }
    
    private void BindDoor()
    {
        var doorView = Container.InstantiatePrefabForComponent<DoorView>(DoorPrefab,
            SpawnPositions.DoorSpawnPosition.position, Quaternion.identity, null);
        
        Container.Bind<DoorView>().FromInstance(doorView).AsSingle().NonLazy();
    }

    private void BindEnemies()
    {
        Container.BindInterfacesAndSelfTo<EnemiesProvider>().AsSingle().NonLazy();
    }
    
    private void BindInputSystems()
    {
        Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle().NonLazy();
    }
    
}