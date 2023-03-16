using Zenject;

public class CommonInstaller : MonoInstaller
{
   
   public override void InstallBindings()
   {
      BindInputSystems();
   }
   
   private void BindInputSystems()
   {
      Container.BindInterfacesAndSelfTo<PlayerInput>().AsSingle().NonLazy();
   }
}