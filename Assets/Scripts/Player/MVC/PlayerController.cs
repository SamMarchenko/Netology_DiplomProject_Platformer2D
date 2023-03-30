using System;
using DefaultNamespace.Factories;
using DefaultNamespace.Signals;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Players.MVC
{
    public class PlayerController : ITickable, IPlayerDamageListener, IDisposable
    {
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly PlayerInput _playerInput;
        private readonly ProjectileFactory _projectileFactory;
        private readonly EnemySignalBus _enemySignalBus;
        private AnimationController _animationController;
        private bool _canMove = true;

        public PlayerController(PlayerModel playerModel, PlayerView playerView,
            PlayerInput playerInput, ProjectileFactory projectileFactory, EnemySignalBus enemySignalBus)
        {
            _playerModel = playerModel;
            _playerView = playerView;
            _playerInput = playerInput;
            _projectileFactory = projectileFactory;
            _enemySignalBus = enemySignalBus;
            _animationController = new AnimationController(_playerView.Animator);
            Subscribe();
            _playerView.ProjectileFactory = _projectileFactory;
            _playerView.BaseDamage = _playerModel.Damage;
        }

        public void Tick()
        {
            if (_canMove)
            {
                if (UpdateDamageTimer())
                {
                    _playerView.Move(_playerModel.MoveSpeed);
                } 
            }
            
        }

        private bool UpdateDamageTimer()
        {
            if (!_playerView.IsDamaged) return true;
            _playerView.DamagedTimer -= Time.deltaTime;
            if (_playerView.DamagedTimer <= 0)
            {
                _playerView.IsDamaged = false;
                _playerView.DamagedTimer = 0.5f;
                return true;
            }

            return false;
        }


        private void Subscribe()
        {
            _playerInput.OnMove += OnMove;
            _playerInput.OnJump += OnJump;
            _playerInput.OnBaseAttack += OnBaseAttack;
            _playerInput.OnStrongAttackStart += OnStrongAttackStart;
            _playerInput.OnStrongAttackEnd += OnStrongAttackEnd;
            _playerView.OnUnderFeetYes += OnUnderFeetYes;
            _playerView.OnUnderFeetNo += OnUnderFeetNo;
            _playerView.OnEnemyAttack += OnEnemyAttack;
        }

        private void OnEnemyAttack(EnemyView enemy, int damage)
        {
            _enemySignalBus.EnemyTakeDamage(new EnemyDamageSignal(enemy, damage));
        }

        private void UnSubscribe()
        {
            _playerInput.OnMove -= OnMove;
            _playerInput.OnJump -= OnJump;
            _playerInput.OnBaseAttack -= OnBaseAttack;
            _playerInput.OnStrongAttackStart -= OnStrongAttackStart;
            _playerInput.OnStrongAttackEnd -= OnStrongAttackEnd;
            _playerView.OnUnderFeetYes -= OnUnderFeetYes;
            _playerView.OnUnderFeetNo -= OnUnderFeetNo;
        }
        private void OnStrongAttackEnd()
        {
            if (!_canMove)
            {
                Debug.Log($"Совершил сильную атаку. {_canMove}");
                _playerView.Attack(EAttackType.StrongAttack);
                _animationController.PlayAnimation(_playerView.MoveDirection == Vector2.zero
                    ? EAnimStates.Idle
                    : EAnimStates.Run);
            }
            _canMove = true;
        }
        
        private void OnStrongAttackStart()
        {
            _canMove = false;
            _playerView.MoveDirection = Vector2.zero;
            _animationController.PlayAnimation(EAnimStates.Idle);
            Debug.Log($"OnStrongAttackStart. {_canMove}");
        }

        private void OnBaseAttack()
        {
            if (_canMove)
            {
                _playerView.Attack(EAttackType.BaseAttack);
            }
        }

        private void OnUnderFeetNo(Collider2D collider)
        {
            if (collider.gameObject.CompareTag("Ground"))
            {
                _playerView.IsGrounded = false;
            }
        }

        private void OnUnderFeetYes(Collider2D collider)
        {
            if (!collider.gameObject.CompareTag("Ground")) return;
            _playerView.IsGrounded = true;
            _playerView.IsJumping = false;
            _playerView.JumpsCount = 0;

            _animationController.PlayAnimation(_playerView.MoveDirection == Vector2.zero
                ? EAnimStates.Idle
                : EAnimStates.Run);
        }

        private void OnJump()
        {
            if (_playerView.IsDamaged)
            {
                return;
            }

            if (_playerView.IsGrounded)
            {
                _playerView.Jump(_playerModel.JumpForce);
                _animationController.PlayAnimation(EAnimStates.Jump);
                return;
            }

            if (_playerView.IsJumping && _playerView.JumpsCount < _playerModel.MaxJumps)
            {
                _playerView.Jump(_playerModel.JumpForce);
                _animationController.PlayAnimation(EAnimStates.Jump);
            }
        }

        private void OnMove(Vector2 direction)
        {
            if (_playerView.IsDamaged || !_canMove)
            {
                return;
            }

            _playerView.MoveDirection = direction;
            if (_playerView.IsGrounded)
            {
                _animationController.PlayAnimation(_playerView.MoveDirection == Vector2.zero
                    ? EAnimStates.Idle
                    : EAnimStates.Run);
            }
        }

        public void OnPlayerDamage(PlayerDamageSignal signal)
        {
            _playerView.TakeDamageVisual();
            if (_playerModel.Health - signal.Damage > 0)
            {
                _playerModel.Health -= signal.Damage;
                Debug.Log($"Игрок получил урон {signal.Damage}. Осталось {_playerModel.Health}");
            }
            else
            {
                _playerModel.Health = 0;
                Debug.Log($"Игрок умер");
            }
        }

        public void Dispose()
        {
            UnSubscribe();
        }
    }
}