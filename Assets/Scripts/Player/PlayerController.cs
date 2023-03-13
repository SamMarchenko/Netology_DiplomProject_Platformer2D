using DefaultNamespace.Player;
using UnityEngine;

public class PlayerController
{
    private PlayerView _playerView;

    public PlayerController(PlayerView playerView)
    {
        _playerView = playerView;
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
    }

    public void PlayerMove(Vector2 direction)
    {
        _playerView.Direction = direction;
    }

    public void PlayerJump()
    {
        if (_playerView.IsGrounded)
        {
            _playerView.Jump();
            return;
        }

        if (_playerView.IsJumping && _playerView.JumpsCount < _playerView.MaxJumps)
        {
            _playerView.Jump();
        }
    }

    

    
}
