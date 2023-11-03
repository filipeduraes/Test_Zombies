using Cinemachine;
using UnityEngine;

namespace Zombie.Camera
{
    public class CameraSetup : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera defaultCamera;
        [SerializeField] private CinemachineVirtualCamera shootCamera;
        [SerializeField] private CameraController cameraController;
        
        public void Initialize(Transform lookDirection)
        {
            defaultCamera.Follow = lookDirection;
            shootCamera.Follow = lookDirection;
            
            cameraController.Initialize(defaultCamera, shootCamera);
            CameraController.SetDefaultCamera();
        }
    }
}