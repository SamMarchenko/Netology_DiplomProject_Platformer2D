using DefaultNamespace.Factories;
using DefaultNamespace.Player;
using UnityEngine;

namespace DefaultNamespace
{
    public class StaticEnemy : IEnemy
    {
        private EnemyType _enemyType;
        private Transform _spawnPosition;
        private EnemyView _enemyView;
        private EnemyController _controller;
        private StaticEnemyFactory _factory;
        
        public EnemyType EnemyType => _enemyType;

        public StaticEnemy(EnemyView enemyView)
        {
            _enemyView = enemyView;
        }
       
    }
}