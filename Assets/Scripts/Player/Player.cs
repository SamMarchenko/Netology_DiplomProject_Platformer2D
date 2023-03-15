using UnityEngine;

namespace DefaultNamespace.Player
{
    public class Player
    {
        private PlayerView _playerView;
        private readonly PlayerInput _playerInput;
        private PlayerController _controller;

        public Player(PlayerInput playerInput, PlayerView playerView, PlayerController controller)
        {
            _playerInput = playerInput;
            _controller = controller;
            _playerView = playerView;
            Subscribe();
            
            //todo: где отписываться не в монобехе?
        }
        
        private void Subscribe()
        {
            _playerInput.OnMove += OnMove;
            _playerInput.OnJump += OnJump;
        }

        private void OnMove(Vector2 direction)
        {
            _controller.PlayerMove(direction);
            
            
        }

        private void OnJump()
        {
            _controller.PlayerJump();
            
        }
    }
}