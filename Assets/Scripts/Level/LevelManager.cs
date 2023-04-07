using System;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace.Doors;
using DefaultNamespace.Loot;
using DefaultNamespace.Players;
using DefaultNamespace.Players.MVC;
using DefaultNamespace.Signals;
using DefaultNamespace.SO;
using ModestTree;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    public class LevelManager : IDisposable, IExitLevelListener
    {
        private readonly EnemiesPresetContainer _enemiesPresetContainer;
        private readonly SpawnPositions _spawnPositions;
        private readonly EnemiesProvider _enemiesProvider;
        private readonly DoorView _door;
        private readonly PlayerView _player;
        private readonly PlayerController _playerController;
        private readonly FailScreenManager _failScreenPrefab;
        private readonly WinScreenManager _winScreenPrefab;
        private readonly InventaryUpdateSignalBus _inventaryUpdateSignalBus;
        private UITopPanelCoreManager _uiTopPanelCoreManager;
        private FailScreenManager _failScreen;
        private WinScreenManager _winScreen;
        private List<EnemyView> _enemies;
        private List<LootView> _loots = new List<LootView>();
        private int _currentLevelNumber = PlayerPrefs.GetInt(SavesStrings.CurrentLevel);
        private bool _isWin;


        public LevelManager(EnemiesProvider enemiesProvider,
            DoorView door,
            PlayerView player,
            PlayerController playerController,
            FailScreenManager failScreenPrefab,
            WinScreenManager winScreenPrefab,
            InventaryUpdateSignalBus inventaryUpdateSignalBus)
        {
            _enemiesProvider = enemiesProvider;
            _door = door;
            _player = player;
            _playerController = playerController;
            _failScreenPrefab = failScreenPrefab;
            _winScreenPrefab = winScreenPrefab;
            _inventaryUpdateSignalBus = inventaryUpdateSignalBus;
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
                enemy.OnCreateLoot += OnCreateLoot;
            }
        }

        private void OnCreateLoot(LootView obj)
        {
            obj.OnPickUpLoot += OnPickUpLoot;
            _loots.Add(obj);
        }
        

        private void OnPickUpLoot(LootView obj)
        {
            switch (obj.LootType)
            {
                case ELootType.Shield:
                    _player.HasShield = true;
                    _inventaryUpdateSignalBus.InventaryUpdate(new InventarySignal(EInventaryType.Shield));
                    PlayerPrefs.SetInt(SavesStrings.PlayerHasShield,1);
                    break;
                case ELootType.Heart:
                    _playerController.HealPlayer();
                    break;
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
            var currentLvl = PlayerPrefs.GetInt(SavesStrings.CurrentLevel);
            PlayerPrefs.SetInt(SavesStrings.CurrentLevel, currentLvl + 1);
            
            _playerController.SaveCurrentHealth();
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

        public void OnExitLevel(ExitLevelSignal signal)
        {
            _playerController.SaveCurrentHealth();
        }
    }
}