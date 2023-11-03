using Cinemachine;
using UnityEngine;

namespace Zombie.Camera
{
    public class CameraController : MonoBehaviour
    {
        private static CinemachineVirtualCamera defaultVirtualCamera;
        private static CinemachineVirtualCamera shootVirtualCamera;
        
        private const int HighPriority = 10;
        private const int LowPriority = 0;

        public void Initialize(CinemachineVirtualCamera defaultCamera, CinemachineVirtualCamera shootCamera)
        {
            defaultVirtualCamera = defaultCamera;
            shootVirtualCamera = shootCamera;
        }
        
        public static void SetDefaultCamera()
        {
            defaultVirtualCamera.Priority = HighPriority;
            shootVirtualCamera.Priority = LowPriority;
            
            defaultVirtualCamera.gameObject.SetActive(true);
            shootVirtualCamera.gameObject.SetActive(false);
        }
        
        public static void SetShootCamera()
        {
            defaultVirtualCamera.Priority = LowPriority;
            shootVirtualCamera.Priority = HighPriority;
            
            defaultVirtualCamera.gameObject.SetActive(false);
            shootVirtualCamera.gameObject.SetActive(true);
        }
    }
}