using System;
using DefaultNamespace.Factories;
using UnityEngine;

namespace DefaultNamespace.Players
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _playerSpriteRenderer;
        [SerializeField] private Transform _projectileSpawnPos;
        private int _currentDamage;


        public EUnitType UnitType => EUnitType.Player;
        public Action<Collider2D> OnUnderFeetYes;
        public Action<Collider2D> OnUnderFeetNo;
        public Action<EnemyView, int> OnEnemyAttack;
        public Animator Animator => _animator;
        public Vector2 MoveDirection = Vector2.zero;
        public int BaseDamage { get; set; }

        public ProjectileFactory ProjectileFactory { get; set; }
        public int JumpsCount { get; set; } = 0;
        public bool IsGrounded;
        public bool IsJumping { get; set; }

        public bool IsDamaged { get; set; } = false;
        public float DamagedTimer { get; set; } = 0.5f;


        public void Move(float speed)
        {
            if (MoveDirection == Vector2.zero)
            {
                return;
            }

            _rigidbody2D.velocity += MoveDirection * speed * Time.deltaTime;
            //TurnPlayerView(MoveDirection);
            Rotate(MoveDirection);
        }

        public void Jump(float jumpForce)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, 0);
            _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            IsJumping = true;
            JumpsCount++;
        }

        public void Attack(EAttackType attackType)
        {
            Vector2 attackDirection;
            var projectile = ProjectileFactory.CreateProjectile(UnitType);
            if (transform.rotation.eulerAngles.y == 180)
            {
                attackDirection = Vector2.left;
            }
            else
            {
                attackDirection = Vector2.right;
            }

            if (attackType == EAttackType.StrongAttack)
            {
                _currentDamage *= 2;
                projectile.transform.localScale *= 2f;
            }
            else
            {
                _currentDamage = BaseDamage;
            }

            projectile.SetMoveDirection(attackDirection);
            projectile.OnCollisionEnemy += ProjectileEnemyCollision;
            projectile.transform.position = _projectileSpawnPos.position;
            projectile.SpriteRenderer.flipX = attackDirection == Vector2.left;
        }

        private void ProjectileEnemyCollision(EnemyView enemy, int damage)
        {
            OnEnemyAttack?.Invoke(enemy, damage);
        }

        public void TurnPlayerView(Vector2 direction) => _playerSpriteRenderer.flipX = direction.x < 0f;

        private void Rotate(Vector2 direction)
        {
            transform.localEulerAngles = direction.x < 0f
                ? new Vector3(0, 180, 0)
                : new Vector3(0, 0, 0);
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            OnUnderFeetYes?.Invoke(collider);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            OnUnderFeetNo?.Invoke(other);
        }

        public void TakeDamageVisual()
        {
            if (IsDamaged)
            {
                return;
            }

            Vector2 offset;
            offset = _playerSpriteRenderer.flipX ? new Vector2(20, 10) : new Vector2(-20, 10);
            _rigidbody2D.AddForce(offset * 10f, ForceMode2D.Impulse);
            IsDamaged = true;
            MoveDirection = Vector2.zero;
        }
    }
}