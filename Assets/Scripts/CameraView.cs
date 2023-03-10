using Cinemachine;
using DefaultNamespace.Player;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [Inject]
        private void Construct(PlayerView playerView)
        {
            _virtualCamera.Follow = playerView.transform;
        }
    }
}