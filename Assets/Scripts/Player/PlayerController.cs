using DefaultNamespace.Player;
using UnityEngine;
using Zenject;

public class PlayerController
{
    private PlayerView _playerView;

    public PlayerController(PlayerView playerView)
    {
        _playerView = playerView;
    }

    public void PlayerMove(Vector2 direction)
    {
        _playerView.Direction = direction;
        //_playerView.Move(direction);
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
