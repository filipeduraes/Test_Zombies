using Cinemachine;
using UnityEngine;

namespace Zombie.Camera
{
    public class CameraSetup : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera defaultCamera;
        [SerializeField] private CinemachineVirtualCamera shootCamera;
        
        public void Initialize(Transform lookDirection)
        {
            defaultCamera.Follow = lookDirection;
            shootCamera.Follow = lookDirection;
        }
    }
}