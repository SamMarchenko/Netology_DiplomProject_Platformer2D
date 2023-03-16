using UnityEngine;

namespace DefaultNamespace
{
    public class EnemyView : MonoBehaviour
    {
        [SerializeField] private EEnemyType _type;
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _playerSpriteRenderer;

        public EEnemyType Type => _type;
        public Animator Animator => _animator;
    }
}