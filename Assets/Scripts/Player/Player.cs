using DefaultNamespace.Factories;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class Player
    {
        private PlayerView _playerView;
        private readonly InputService _input;
        private PlayerController _controller;

        public Player(InputService input, PlayerView playerView, PlayerController controller)
        {
            _input = input;
            _controller = controller;
            _playerView = playerView;
            Subscribe();
            
            //todo: где отписываться не в монобехе?
        }
        
        private void Subscribe()
        {
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