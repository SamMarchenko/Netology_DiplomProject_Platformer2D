using Cinemachine;
using UnityEngine;
using Zenject;

public class LocationInstaller : MonoInstaller
{
    public Transform StartPoint;
    public GameObject HeroPrefab;
    //[SerializeField] private CinemachineVirtualCamera _virtualCameraPrefab;

    public override void InstallBindings()
    {
        BindHero();
        //BindVirtualCamera();
    }

    

    private void BindHero()
    {
        var playerController = Container.InstantiatePrefabForComponent<PlayerController>(HeroPrefab,
            StartPoint.position, Quaternion.identity, null);

        Container.Bind<PlayerController>().FromInstance(playerController).AsSingle();
    }
    
    
    
    // private void BindVirtualCamera()
    // {
    //     var virtualCamera =
    //         Container.InstantiatePrefabForComponent<CinemachineVirtualCamera>(_virtualCameraPrefab);
    //     Container.Bind<CinemachineVirtualCamera>().FromInstance(virtualCamera).AsSingle();
    // }
}