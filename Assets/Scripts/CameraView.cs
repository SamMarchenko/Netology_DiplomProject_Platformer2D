using Cinemachine;
using DefaultNamespace.Players;
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
