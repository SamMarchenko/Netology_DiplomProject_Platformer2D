using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Doors;
using DefaultNamespace.Players;
using DefaultNamespace.SO;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelManager
    {
        private readonly EnemiesPresetContainer _enemiesPresetContainer;
        private readonly SpawnPositions _spawnPositions;
        private readonly EnemiesProvider _enemiesProvider;
        private readonly DoorView _door;
        private readonly PlayerView _player;
        private readonly FailScreenManager _failScreenPrefab;
        private readonly WinScreenManager _winScreenPrefab;
        private FailScreenManager _failScreen;
        private WinScreenManager _winScreen;
        private List<EnemyView> _enemies;
        private int _currentLevelNumber = PlayerPrefs.GetInt("currentLevel");
        private bool _isWin;


        public LevelManager(EnemiesProvider enemiesProvider, DoorView door, PlayerView player, FailScreenManager failScreenPrefab, WinScreenManager winScreenPrefab)
        {
            _enemiesProvider = enemiesProvider;
            _door = door;
            _player = player;
            _failScreenPrefab = failScreenPrefab;
            _winScreenPrefab = winScreenPrefab;
            _enemies = _enemiesProvider.GetEnemies(_currentLevelNumber);
            SubscribeEnemies();
            SubscribeDoor();
            SubscribePlayer();
        }

        private void SubscribeEnemies()
        {
            foreach (var enemy in _enemies)
            {
                enemy.OnDead += OnDead;
            }
        }

        private void SubscribeDoor()
        {
            _door.OnPlayerEntered += OnPlayerEnteredDoor;
        }

        private void SubscribePlayer()
        {
            _player.OnDeath += OnDeath;
        }

        private void OnDeath()
        {
            if (_isWin)
            {
                return;
            }
            Debug.Log("Игрок умер");
            _failScreen = MonoBehaviour.Instantiate(_failScreenPrefab);
            //EditorApplication.isPaused = true;
        }

        private void OnPlayerEnteredDoor()
        {
            _isWin = true;
            var currentLvl = PlayerPrefs.GetInt("currentLevel");
            PlayerPrefs.SetInt("currentLevel", currentLvl + 1);

            //todo: запихнуть сюда экран победы
            _winScreen = MonoBehaviour.Instantiate(_winScreenPrefab);
            //
            // if ( PlayerPrefs.GetInt("currentLevel") <= PlayerPrefs.GetInt("levelsCount"))
            // {
            //     SceneTransition.SwitchToScene("Level" + PlayerPrefs.GetInt("currentLevel"));
            // }
            // else
            // {
            //     SceneTransition.SwitchToScene("MainMenu");
            // }
            
        }

        private void OnDead(EnemyView view)
        {
            UnsubscribeEnemy(view);
            _enemies.Remove(view);

            if (!view.IsRequiredKilling) return;

            if (_enemies.Any(enemy => enemy.IsRequiredKilling))
            {
                return;
            }

            _door.Open();
        }

        private void UnsubscribeEnemy(EnemyView view)
        {
            foreach (var enemy in _enemies)
            {
                if (enemy == view)
                {
                    enemy.OnDead -= OnDead;
                    return;
                }
            }
        }
    }
}