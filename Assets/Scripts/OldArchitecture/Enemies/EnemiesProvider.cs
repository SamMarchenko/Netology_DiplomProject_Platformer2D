using System;
using System.Collections.Generic;
using DefaultNamespace;
using DefaultNamespace.Factories;
using DefaultNamespace.SO;
using ModestTree;
using UnityEngine;

public class EnemiesProvider
{
    private readonly EnemiesPresetContainer _enemiesPresetContainer;
    private readonly EnemyFactory _enemyFactory;
    private List<EnemyView> _createdEnemies = new List<EnemyView>();


    public EnemiesProvider(EnemiesPresetContainer enemiesPresetContainer,
        EnemyFactory enemyFactory)
    {
        _enemiesPresetContainer = enemiesPresetContainer;
        _enemyFactory = enemyFactory;
    }

    public List<EnemyView> GetEnemies(int levelNumber)
    { 
        if (!_createdEnemies.IsEmpty())
        {
            return _createdEnemies;
        }
        var enemiesData =  GetEnemiesPreset(levelNumber);

        foreach (var enemyData in enemiesData)
        {
            _createdEnemies.Add(_enemyFactory.CreateEnemy(enemyData));  
        }

        return _createdEnemies;
    }
    
    private List<EnemyData> GetEnemiesPreset(int levelNumber)
    {
        foreach (var enemiesPreset in _enemiesPresetContainer.EnemiesPresets)
        {
            if (enemiesPreset.LevelNumber == levelNumber)
            {
                return enemiesPreset.EnemiesData;
            }
        }
        Debug.LogException(new Exception("Нет пресета врагов с текущим номером уровня!"));
        return null;
    }
    
}