using DefaultNamespace.Factories;
using DefaultNamespace.Signals;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Players.MVC
{
    public class PlayerController : ITickable, IPlayerDamageListener
    {
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly PlayerInput _playerInput;
        private readonly ProjectileFactory _projectileFactory;
        private AnimationController _animationController;

        public PlayerController(PlayerModel playerModel, PlayerView playerView,
            PlayerInput playerInput, ProjectileFactory projectileFactory)
        {
            _playerModel = playerModel;
            _playerView = playerView;
            _playerInput = playerInput;
            _projectileFactory = projectileFactory;
            _animationController = new AnimationController(_playerView.Animator);

            Subscribe();
        }

        public void Tick()
        {
            if (UpdateDamageTimer())
            {
                _playerView.Move(_playerModel.MoveSpeed);
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
        _playerView.OnUnderFeetYes += OnUnderFeetYes;
        _playerView.OnUnderFeetNo += OnUnderFeetNo;
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
        if (_playerView.IsDamaged)
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
    }

}