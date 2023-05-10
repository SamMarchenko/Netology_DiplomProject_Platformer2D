using Refactor.States;
using UnityEngine;
using Zenject;

public class CommonInstaller : MonoInstaller
{
   [SerializeField] private FailScreenManager _failScreenPrefab;
   [SerializeField] private WinScreenManager _winScreenPrefab;
   [SerializeField] private LoadingCurtain _curtain;


   public override void InstallBindings()
   {
      Container.BindInstance(_failScreenPrefab);
      Container.BindInstance(_winScreenPrefab);
      Container.BindInstance(_curtain);
   }
   
  
   
  
 
}