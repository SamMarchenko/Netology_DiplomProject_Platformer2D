using System;
using DefaultNamespace.Factories;
using DefaultNamespace.Projectiles;
using DG.Tweening;
using UnityEditor.Animations;
using UnityEngine;

namespace DefaultNamespace.Players
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private SpriteRenderer _currentSpriteRenderer;
        [SerializeField] private SpriteRenderer[] _playerSpriteRenderers;
        [SerializeField] private SpriteRenderer _blockSprite;
        [SerializeField] private Transform _projectileSpawnPos;
        [SerializeField] private AnimatorController[] _animationControllers;
        private int _currentDamage;
        private int _currentTransformView = 0;
        private bool _isTransformed;


        public int CurrentTransformView => _currentTransformView;
        public SpriteRenderer BlockSprite => _blockSprite;
        public EUnitType UnitType => EUnitType.Player;
        public Action<Collider2D> OnUnderFeetYes;
        public Action<Collider2D> OnUnderFeetNo;
        public Action<EnemyView, int> OnEnemyAttack;
        public Action OnDeath;
        public Animator Animator => _animator;
        public Vector2 MoveDirection = Vector2.zero;
        public int BaseDamage { get; set; }

        public ProjectileFactory ProjectileFactory { get; set; }
        public int JumpsCount { get; set; } = 0;
        public bool IsGrounded;
        public bool HasShield;
        public bool IsJumping { get; set; }

        public bool IsDamaged { get; set; } = false;
        public float DamagedTimer { get; set; } = 0.5f;

        private void Awake()
        {
            _blockSprite.DOFade(0, 0);
        }

        private void Start()
        {
            HasShield = PlayerPrefs.GetInt("PlayerHasShield") == 1;
            
            _currentSpriteRenderer = _playerSpriteRenderers[_currentTransformView];
            _playerSpriteRenderers[_currentTransformView + 1].gameObject.SetActive(false);
            _animator.runtimeAnimatorController = _animationControllers[_currentTransformView];
        }

        public void Move(float speed)
        {
            if (MoveDirection == Vector2.zero)
            {
                return;
            }

            _rigidbody2D.velocity += MoveDirection * speed * Time.deltaTime;
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
            if (_isTransformed)
            {
                TransfromedUnitAttack(attackType);
            }
            else
            {
                BaseFormUnitAttack(attackType);
            }
        }

        public void Death()
        {
            OnDeath?.Invoke();
        }

        private void TransfromedUnitAttack(EAttackType attackType)
        {
            var projectile = ProjectileFactory.CreateProjectile(EUnitType.TransformedPlayer, EProjectileType.Melee);
            projectile.transform.SetParent(_projectileSpawnPos);
            
            var attackDirection = ProjectileStartSettings(attackType, projectile, EProjectileType.Melee);
            projectile.SetMoveDirection(attackDirection);
            Debug.Log($"Атака трансформированного юнита с типом {attackType}");
            
            projectile.OnCollisionEnemy += ProjectileEnemyCollision;
            projectile.transform.localPosition = Vector3.zero;
           // projectile.SpriteRenderer.flipX = attackDirection == Vector2.left;
        }

        private void BaseFormUnitAttack(EAttackType attackType)
        {
            var projectile = ProjectileFactory.CreateProjectile(UnitType, EProjectileType.Range);

            var attackDirection = ProjectileStartSettings(attackType, projectile, EProjectileType.Range);

            projectile.SetMoveDirection(attackDirection);
            projectile.OnCollisionEnemy += ProjectileEnemyCollision;
            projectile.transform.position = _projectileSpawnPos.position;
            projectile.SpriteRenderer.flipX = attackDirection == Vector2.left;
        }

        private Vector2 ProjectileStartSettings(EAttackType attackType, ProjectileView projectile,
            EProjectileType type)
        {
            Vector2 attackDirection;
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
                switch (type)
                {
                    case EProjectileType.Melee:
                        _currentDamage *= 2;
                        projectile.transform.localScale *= 3f;
                        break;
                    case EProjectileType.Range:
                        _currentDamage *= 2;
                        projectile.transform.localScale *= 2f;
                        break;
                }
                
            }
            else
            {
                _currentDamage = BaseDamage;
            }

            if (type == EProjectileType.Melee)
            {
                projectile.transform.localEulerAngles = new Vector3(0, 0, 0);
            }
            // projectile.transform.localEulerAngles = attackDirection.x < 0f
            //     ? new Vector3(0, 180, 0)
            //     : new Vector3(0, 0, 0);

            return attackDirection;
        }

        private void ProjectileEnemyCollision(EnemyView enemy, int damage)
        {
            OnEnemyAttack?.Invoke(enemy, damage);
        }


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
            offset = _currentSpriteRenderer.flipX ? new Vector2(20, 10) : new Vector2(-20, 10);
            _rigidbody2D.AddForce(offset * 10f, ForceMode2D.Impulse);
            IsDamaged = true;
            MoveDirection = Vector2.zero;
        }

        public void Transfromed()
        {
            //вызывается при окончании анимации трансформации
            if (_currentTransformView == 0)
            {
                _currentTransformView++;
                _isTransformed = true;
            }
            else
            {
                _currentTransformView--;
                _isTransformed = false;
            }

            _currentSpriteRenderer.gameObject.SetActive(false);
            _currentSpriteRenderer = _playerSpriteRenderers[_currentTransformView];
            _currentSpriteRenderer.gameObject.SetActive(true);
            _animator.runtimeAnimatorController = _animationControllers[_currentTransformView];
        }
    }
}