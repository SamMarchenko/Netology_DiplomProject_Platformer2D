using DefaultNamespace.Factories;
using UnityEngine;

namespace DefaultNamespace.Player
{
    public class Player
    {
        private PlayerView _playerView;
        private readonly SpawnPositions _spawnPositions;
        private readonly PlayerViewFactory _factory;
        private readonly InputService _input;
        private PlayerController _controller;

        public Player(InputService input, PlayerView playerView,
            SpawnPositions spawnPositions, PlayerViewFactory factory)
        {
            _input = input;
            
            _playerView = playerView;
            _spawnPositions = spawnPositions;
            _factory = factory;

            Subscribe();
            CreatePlayerView(spawnPositions);
            _controller = new PlayerController(_playerView);
            //todo: где отписываться не в монобехе?
        }

        private void CreatePlayerView(SpawnPositions spawnPositions)
        {
          _playerView = _factory.CreatePlayer(_playerView, spawnPositions.PlayerSpawnPos);
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