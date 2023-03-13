using DefaultNamespace.Factories;
using DefaultNamespace.Player;
using UnityEngine;

namespace DefaultNamespace
{
    public class StaticEnemy : IEnemy
    {
        private EEnemyType _eEnemyType;
        private Transform _spawnPosition;
        private EnemyView _enemyView;
        private EnemyController _controller;
        private StaticEnemyFactory _factory;
        
        public EEnemyType EEnemyType => _eEnemyType;

        public StaticEnemy(EnemyView enemyView)
        {
            _enemyView = enemyView;
        }
       
    }
}