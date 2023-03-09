using Cinemachine;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [Inject]
        private void Construct(PlayerController playerController)
        {
            _virtualCamera.Follow = playerController.transform;
        }
    }
}