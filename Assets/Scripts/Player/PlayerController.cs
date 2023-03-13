using DefaultNamespace;
using DefaultNamespace.Player;
using UnityEngine;

public class PlayerController
{
    private PlayerView _playerView;
    private AnimationController _animationController;

    public PlayerController(PlayerView playerView, AnimationController animationController)
    {
        _playerView = playerView;
        _animationController = animationController;
        _animationController.Init(_playerView.Animator);
        Subscribe();
        //todo: где отписываться не в монобехе?
    }

    private void Subscribe()
    {
        _playerView.OnUnderFeetYes += OnUnderFeet;
        _playerView.OnUnderFeetNo += OnUnderFeetNo;
    }

    private void OnUnderFeetNo(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _playerView.IsGrounded = false;
        }
    }

    private void OnUnderFeet(Collider2D collider)
    {
        if (!collider.gameObject.CompareTag("Ground")) return;
        _playerView.IsGrounded = true;
        _playerView.IsJumping = false;
        _playerView.JumpsCount = 0;

        _animationController.PlayAnimation(_playerView.Direction == Vector2.zero ? EAnimStates.Idle : EAnimStates.Run);
    }

    public void PlayerMove(Vector2 direction)
    {
        _playerView.Direction = direction;

        _animationController.PlayAnimation(direction == Vector2.zero ? EAnimStates.Idle : EAnimStates.Run);
    }

    public void PlayerJump()
    {
        if (_playerView.IsGrounded)
        {
            _playerView.Jump();
            _animationController.PlayAnimation(EAnimStates.Jump);
            return;
        }

        if (_playerView.IsJumping && _playerView.JumpsCount < _playerView.MaxJumps)
        {
            _playerView.Jump();
            _animationController.PlayAnimation(EAnimStates.Jump);
        }
    }
}