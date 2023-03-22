using System;
using DefaultNamespace.Players;
using UnityEngine;

namespace DefaultNamespace
{
    public class PeekOutEnemyView : EnemyView
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _enemySpriteRenderer;
        [SerializeField] private Transform _projectileSpawnPos;
        [SerializeField] private GameObject _attentionSprite;
        private float _rotateTimer = 3f;
        private float _attackCooldown = 0.5f;
        
        public Animator Animator => _animator;
        public Transform ProjectileSpawnPos => _projectileSpawnPos;
        public GameObject AttentionSprite => _attentionSprite;

        private void Update()
        {
            if (!HasTarget)
            {
                Rotate();
            }
            else
            {
                Attack();
            }
        }

        private void Rotate()
        {
            _rotateTimer -= Time.deltaTime;
            if (_rotateTimer <= 0f)
            {
                transform.localEulerAngles = transform.rotation.y == 0 
                    ? new Vector3(0, 180, 0) 
                    : new Vector3(0, 0, 0);
                _rotateTimer = 3f;
            }
        }


        public void Attack()
        {
            _attackCooldown -= Time.deltaTime;
            if (_attackCooldown > 0)
            {
                return;
            }
            Vector2 attackDirection;
            var projectile = ProjectileFactory.CreateProjectile();
            if (transform.rotation.y == 0)
            {
                attackDirection = Vector2.left;
            }
            else
            {
                attackDirection = Vector2.right;
            }
            projectile.Init(UnitType, attackDirection, 20f);
            projectile.transform.position = _projectileSpawnPos.position;
            projectile.SpriteRenderer.flipX = attackDirection == Vector2.left;
            _attackCooldown = 2f;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (!col.gameObject.CompareTag("Player")) return;
            _target = col.transform;
            OnFindTarget?.Invoke();
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;

            _target = null;
            OnLoseTarget?.Invoke();
        }
        
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Debug.Log("Это игрок");
                var player = col.gameObject.GetComponent<PlayerView>();
                player.TakeDamageVisual();
                OnConnectWithPlayer?.Invoke();
            }
        }
    }
}