using UnityEngine;
using Zenject;

public class CommonInstaller : MonoInstaller
{
   [SerializeField] private GameObject _failScreenPrefab;
   public override void InstallBindings()
   {
      Container.BindInstance(_failScreenPrefab);
   }
   
 
}