using UnityEngine;

namespace DefaultNamespace.Player
{
    public class Player
    {
        private readonly PlayerView _playerView;
        private readonly InputService _input;
        private PlayerController _controller;

        public Player(InputService input ,PlayerController controller, PlayerView playerView)
        {
            _input = input;
            _controller = controller;
            _playerView = playerView;
            
            _input.OnMove += OnMove;
            _input.OnJump += OnJump;
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