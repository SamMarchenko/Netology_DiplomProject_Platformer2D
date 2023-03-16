using UnityEngine;
using Zenject;

namespace DefaultNamespace.Players.MVC
{
    public class PlayerController : ITickable
    {
        private readonly PlayerModel _playerModel;
        private readonly PlayerView _playerView;
        private readonly PlayerInput _playerInput;
        private AnimationController _animationController;

        public PlayerController(PlayerModel playerModel, PlayerView playerView,
            PlayerInput playerInput, AnimationController animationController)
        {
            _playerModel = playerModel;
            _playerView = playerView;
            _playerInput = playerInput;
            _animationController = animationController;
            _animationController.Init(_playerView.Animator);
            
            Subscribe();
        }
        
        public void Tick()
        {
            _playerView.Move(_playerModel.MoveSpeed);
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

            _animationController.PlayAnimation(_playerView.MoveDirection == Vector2.zero ? EAnimStates.Idle : EAnimStates.Run);
        }

        private void OnJump()
        {
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
            _playerView.MoveDirection = direction;
            if (_playerView.IsGrounded)
            {
                _animationController.PlayAnimation(_playerView.MoveDirection == Vector2.zero ? EAnimStates.Idle : EAnimStates.Run);
            }
        }
        
    }
}