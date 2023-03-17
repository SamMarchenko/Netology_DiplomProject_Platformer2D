using UnityEngine;

namespace DefaultNamespace
{
    public class WalkingEnemyView : EnemyView
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _playerSpriteRenderer;
        
        public Animator Animator => _animator;
    }
}