using System;
using DefaultNamespace.Players;
using UnityEngine;

namespace DefaultNamespace.Projectiles
{
    public class ProjectileView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private EUnitType _owner;
        private Vector2 _moveDirection;
        private float _attackSpeed;
        private int _damage;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Action OnCollisionPlayer;
        public Action<EnemyView, int> OnCollisionEnemy;

        public void Init(EUnitType owner, float attackSpeed, int damage)
        {
            _owner = owner;
            _attackSpeed = attackSpeed;
            _damage = damage;
        }

        public void SetMoveDirection(Vector2 moveDirection)
        {
            _moveDirection = moveDirection;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position = Vector3.MoveTowards(transform.position, transform.position + (Vector3) _moveDirection,
                _attackSpeed * Time.deltaTime);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.isTrigger) return;
            
            if (!col.gameObject.CompareTag("Enemy") && !col.gameObject.CompareTag("Player"))
            {
                Debug.Log("Снаряд врезался в препятствие!");
                Destroy(gameObject);
                return;
            }

            if (col.gameObject.CompareTag("Enemy") && _owner == EUnitType.Player)
            {
                var enemy = col.gameObject.GetComponent<EnemyView>();
                OnCollisionEnemy?.Invoke(enemy, _damage);
                Destroy(gameObject);
                return;
            }

            if (col.gameObject.CompareTag("Player") && _owner == EUnitType.Enemy)
            {
                var player = col.gameObject.GetComponent<PlayerView>();
                
                if (player.IsDamaged)
                {
                    return;
                }
                OnCollisionPlayer?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}