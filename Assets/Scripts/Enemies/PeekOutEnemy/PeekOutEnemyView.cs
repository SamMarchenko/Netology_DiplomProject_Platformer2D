using UnityEngine;

namespace DefaultNamespace
{
    public class PeekOutEnemyView : EnemyView
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _playerSpriteRenderer;
        
        public Animator Animator => _animator;
    }
}