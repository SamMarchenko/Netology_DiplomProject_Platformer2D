using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Factories;
using DefaultNamespace.Players;
using DefaultNamespace.Players.MVC;
using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    public SpawnPositions SpawnPositions;
    public PlayerView PlayerPrefab;
   

    public override void InstallBindings()
    
    {
        Container.BindInstance(SpawnPositions);
        BindFactories();
        BindEnemies();
        BindPlayer();
        Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle().NonLazy();
    }

    private void BindFactories()
    {
        Container.BindInterfacesAndSelfTo<EnemyFactory>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PeekOutEnemyFactory>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PassiveEnemyFactory>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<WalkingEnemyFactory>().AsSingle().NonLazy();
    }

    
    private void BindPlayer()
    {
        
        //todo: Убрать создание вьюхи из инсталлера
        var playerView = Container.InstantiatePrefabForComponent<PlayerView>(PlayerPrefab,
            SpawnPositions.PlayerSpawnPos.position, Quaternion.identity, null);
        
        Container.Bind<PlayerView>().FromInstance(playerView).AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerModel>().AsSingle().NonLazy();
    }

    private void BindEnemies()
    {
        Container.BindInterfacesAndSelfTo<EnemiesProvider>().AsSingle().NonLazy();
    }
    
}