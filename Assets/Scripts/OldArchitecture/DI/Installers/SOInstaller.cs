using System.Collections.Generic;
using DefaultNamespace.SO;
using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "SOInstaller", menuName = "Installers/SOInstaller")]
public class SOInstaller : ScriptableObjectInstaller<SOInstaller>
{
    [SerializeField] private PlayerData _playerData;
    [SerializeField] private EnemiesPresetContainer _enemiesPresetContainer;
    [SerializeField] private ProjectilesPreset _projectilesPreset;
    public override void InstallBindings()
    {
        BindPlayerData();
        BindEnemiesData();
        BindProjectiles();
    }

    private void BindEnemiesData()
    {
        Container.BindInstance(_enemiesPresetContainer);
    }

    private void BindPlayerData()
    {
        Container.BindInstance(_playerData);
    }

    private void BindProjectiles()
    {
        Container.BindInstance(_projectilesPreset);
    }
}