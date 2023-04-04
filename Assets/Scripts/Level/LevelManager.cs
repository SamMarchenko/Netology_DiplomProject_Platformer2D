using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Doors;
using DefaultNamespace.Loot;
using DefaultNamespace.Players;
using DefaultNamespace.SO;
using ModestTree;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelManager : IDisposable
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
        private List<LootView> _loots = new List<LootView>();
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
           // SubscribeLoots();
        }

        private void SubscribeEnemies()
        {
            foreach (var enemy in _enemies)
            {
                enemy.OnDead += OnDead;
                enemy.OnCreateLoot += OnCreateLoot;
            }
        }

        private void OnCreateLoot(LootView obj)
        {
            obj.OnPickUpLoot += OnPickUpLoot;
            _loots.Add(obj);
        }
        

        // private void SubscribeLoots()
        // {
        //     foreach (var enemy in _enemies)
        //     {
        //         if (enemy.Loot != null)
        //         {
        //             _loots.Add(enemy.Loot);
        //         }
        //     }
        //
        //     if (!_loots.IsEmpty())
        //     {
        //         foreach (var lootView in _loots)
        //         {
        //             lootView.OnPickUpLoot += OnPickUpLoot;
        //         }
        //     }
        // }

        private void OnPickUpLoot(LootView obj)
        {
            if (obj.LootType == ELootType.Shield)
            {
                _player.HasShield = true;
                PlayerPrefs.SetInt("PlayerHasShield",1);
            }
            UnSubscribeLoot(obj);
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
        }

        private void OnPlayerEnteredDoor()
        {
            _isWin = true;
            var currentLvl = PlayerPrefs.GetInt("currentLevel");
            PlayerPrefs.SetInt("currentLevel", currentLvl + 1);
            
            _winScreen = MonoBehaviour.Instantiate(_winScreenPrefab);
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

        
        

        public void Dispose()
        {
            UnSubscribeDoor();
            UnSubscribeLoots();
            UnSubscribePlayer();
            UnSubscribeEnemies();
        }

        private void UnSubscribePlayer()
        {
            _player.OnDeath -= OnDeath;
        }

        private void UnSubscribeLoots()
        {
            if (!_loots.IsEmpty())
            {
                foreach (var lootView in _loots)
                {
                    lootView.OnPickUpLoot -= OnPickUpLoot;
                }
            }
        }
        
        private void UnSubscribeLoot(LootView lootView)
        {
            foreach (var loot in _loots)
            {
                if (loot == lootView)
                {
                    loot.OnPickUpLoot -= OnPickUpLoot;
                    return;
                }
            }
        }

        private void UnSubscribeDoor()
        {
            _door.OnPlayerEntered -= OnPlayerEnteredDoor;
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

        private void UnSubscribeEnemies()
        {
            if (!_enemies.IsEmpty())
            {
                foreach (var enemy in _enemies)
                {
                    enemy.OnDead -= OnDead;
                }
            }
        }
    }
}