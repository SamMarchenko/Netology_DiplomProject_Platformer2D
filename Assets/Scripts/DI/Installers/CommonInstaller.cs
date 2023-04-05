using UnityEngine;
using Zenject;

public class CommonInstaller : MonoInstaller
{
   [SerializeField] private FailScreenManager _failScreenPrefab;
   [SerializeField] private WinScreenManager _winScreenPrefab;
  
   public override void InstallBindings()
   {
      Container.BindInstance(_failScreenPrefab);
      Container.BindInstance(_winScreenPrefab);
      
     
   }
   
  
 
}