using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Player;
using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    public SpawnPositions SpawnPositions;
    public PlayerView PlayerPrefab;
    public List<EnemyView> EnemiesPrefabs;

    public override void InstallBindings()
    {
        Container.BindInstance(SpawnPositions);
        Container.BindInstance(EnemiesPrefabs);
        BindPlayer();
        Container.BindInterfacesAndSelfTo<EnemiesCreator>().AsSingle().NonLazy();
    }

    private void BindPlayer()
    {
        
        //todo: Убрать создание вьюхи из инсталлера
        var playerView = Container.InstantiatePrefabForComponent<PlayerView>(PlayerPrefab,
            SpawnPositions.PlayerSpawnPos.position, Quaternion.identity, null);
        
        Container.Bind<PlayerView>().FromInstance(playerView).AsSingle();
        Container.BindInterfacesAndSelfTo<AnimationController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<Player>().AsSingle().NonLazy();
    }
    
}