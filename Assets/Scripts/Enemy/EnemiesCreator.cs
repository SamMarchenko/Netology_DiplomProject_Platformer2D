using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using Random = System.Random;

//todo: это временный создатель врагов на карте для тестирования вьюх. Переделать
public class EnemiesCreator
{
    private readonly SpawnPositions _spawnPositions;
    private List<EnemyView> _enemiesPrefabs;

    public EnemiesCreator(SpawnPositions spawnPositions, List<EnemyView> enemiesPrefabs)
    {
        _spawnPositions = spawnPositions;
        _enemiesPrefabs = enemiesPrefabs;
        CreateEnemies();
    }

    private void CreateEnemies()
    {
        int enemyCount = 0;
        foreach (var spawnPos in _spawnPositions.EnemiesSpawnPositions)
        {
           
            MonoBehaviour.Instantiate(_enemiesPrefabs[enemyCount], spawnPos);
            enemyCount++;
            if (enemyCount == _enemiesPrefabs.Count)
            {
                enemyCount = 0;
            }
        }
    }
    
}
