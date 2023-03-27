using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class EnemyData
    {
        public EnemyView Prefab;
        public EEnemyType Type;
        public int MaxHealth;
        public float MoveSpeed;
        public int CollisionDamage;
        public int ProjectileDamage;
        public float AtackSpeed;
        public Transform SpawnPosition;
    }
}