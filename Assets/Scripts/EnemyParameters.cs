using System;

namespace DefaultNamespace
{
    [Serializable]
    public class EnemyParameters
    {
        public EnemyType EnemyType;
        public int Health;
        public float MoveSpeed = 200f;
        public int Damage;
    }
}