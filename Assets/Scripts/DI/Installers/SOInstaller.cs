using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
{
    [SerializeField] private PlayerData _playerData;
    public override void InstallBindings()
    {
        BindPlayerData();
    }

    private void BindPlayerData()
    {
        Container.BindInstance(_playerData);
    }
}