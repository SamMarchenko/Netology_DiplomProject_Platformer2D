using UnityEngine;

namespace DefaultNamespace.SO
{
    [CreateAssetMenu(fileName = "NewEnemyConfig", menuName = "CharactersConfigs/EnemyConfig")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private float _moveSpeed = 200f;
        [SerializeField] private int _damage;
        
        public int Health => _health;
        public float MoveSpeed => _moveSpeed;
        public int Damage => _damage;
    }
}