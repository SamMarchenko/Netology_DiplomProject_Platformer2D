using System;
using System.Collections.Generic;
using DefaultNamespace.SO;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelManager
    {
        private readonly EnemiesPresetContainer _enemiesPresetContainer;
        private readonly SpawnPositions _spawnPositions;
        private readonly EnemiesProvider _enemiesProvider;

        private List<EnemyView> _enemies;

        //todo: наверно в будущем номер уровня будет передаваться из мета сцены при загрузке кора
        private int _currentLevelNumber = 1;


        public LevelManager( EnemiesProvider enemiesProvider)
        {
            _enemiesProvider = enemiesProvider;
            _enemies = _enemiesProvider.CreateEnemies(_currentLevelNumber);
        }
    }
}