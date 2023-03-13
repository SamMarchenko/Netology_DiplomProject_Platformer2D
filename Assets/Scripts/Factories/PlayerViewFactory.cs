using DefaultNamespace.Player;
using UnityEngine;

namespace DefaultNamespace.Factories
{
    public class PlayerViewFactory
    {
        public PlayerView CreatePlayer(PlayerView _playerViewPrefab,Transform spawnPos)
        {
            return MonoBehaviour.Instantiate(_playerViewPrefab, spawnPos);
        }
    }
}