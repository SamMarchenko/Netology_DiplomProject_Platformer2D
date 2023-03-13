using UnityEngine;

namespace DefaultNamespace.SO
{
    [CreateAssetMenu(fileName = "NewPlayerConfig", menuName = "CharactersConfigs/PlayerConfig")]
    public class PlayerConfig : ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private float _moveSpeed = 400f;
        [SerializeField] private float _jumpForce = 180f;

        public int Health => _health;
        public float MoveSpeed => _moveSpeed;
        public float JumpForce => _jumpForce;
    }
    
}