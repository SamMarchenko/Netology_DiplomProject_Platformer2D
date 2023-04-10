using System;
using DefaultNamespace.Players;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Projectiles
{
    public class ProjectileView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private EUnitType _owner;
        private EProjectileType _type;
        private Vector2 _moveDirection;
        private float _attackSpeed;
        private int _damage;
        private float _liveTime;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Action OnCollisionPlayer;
        public Action<EnemyView, int> OnCollisionEnemy;

        public void Init(EUnitType owner, EProjectileType type, float attackSpeed, int damage)
        {
            _owner = owner;
            _type = type;
            _attackSpeed = attackSpeed;
            _damage = damage;
            SetProjectileLiveTime(owner);
        }

        private void SetProjectileLiveTime(EUnitType owner)
        {
            if (owner == EUnitType.Player)
            {
                _liveTime = 3f;
            }

            if (owner == EUnitType.Enemy)
            {
                _liveTime = 7f;
            }
        }

        public void SetMoveDirection(Vector2 moveDirection)
        {
            _moveDirection = moveDirection;
        }

        private void Update()
        {
            _liveTime -= Time.deltaTime;
            if (_liveTime <= 0 && _owner != EUnitType.TransformedPlayer)
            {
                Destroy(gameObject);
            }
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
                if (_type == EProjectileType.Melee)
                {
                    return;
                }

                Debug.Log("Снаряд врезался в препятствие!");
                DestroyProjectile();
                return;
            }

            if (col.gameObject.CompareTag("Enemy") && (_owner == EUnitType.Player || _owner == EUnitType.TransformedPlayer) )
            {
                var enemy = col.gameObject.GetComponent<EnemyView>();
                OnCollisionEnemy?.Invoke(enemy, _damage);
                DestroyProjectile();
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
                DestroyProjectile();
            }
        }

        public void DestroyProjectile()
        {
            _spriteRenderer.DOFade(0, 0.2f).OnComplete(() => Destroy(gameObject));
        }
    }
}