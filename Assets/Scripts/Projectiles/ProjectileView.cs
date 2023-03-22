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

        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        public void Init(EUnitType owner, Vector2 moveDirection, float attackSpeed)
        {
            _owner = owner;
            _moveDirection = moveDirection;
            _attackSpeed = attackSpeed;
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
                Debug.Log("Снаряд игрока попал в врага");
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
                player.TakeDamageVisual();
                Destroy(gameObject);
            }
        }
    }
}