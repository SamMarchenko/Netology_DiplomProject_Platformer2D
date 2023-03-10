using DefaultNamespace.Player;
using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    public Transform StartPoint;
    public GameObject HeroPrefab;

    public override void InstallBindings()
    {
        BindPlayer();
    }

    

    private void BindPlayer()
    {
        var playerView = Container.InstantiatePrefabForComponent<PlayerView>(HeroPrefab,
            StartPoint.position, Quaternion.identity, null);
        
        Container.Bind<PlayerView>().FromInstance(playerView).AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerController>().AsSingle().NonLazy();
        Container.BindInterfacesAndSelfTo<Player>().AsSingle().NonLazy();
    }
    
}