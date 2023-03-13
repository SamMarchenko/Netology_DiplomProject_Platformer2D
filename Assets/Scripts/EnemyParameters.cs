using System;

namespace DefaultNamespace
{
    [Serializable]
    public class EnemyParameters
    {
        public EEnemyType eEnemyType;
        public int Health;
        public float MoveSpeed = 200f;
        public int Damage;
    }
}