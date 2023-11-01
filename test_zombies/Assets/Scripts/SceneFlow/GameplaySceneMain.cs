using UnityEngine;
using Zombie.Camera;
using Zombie.Player;

namespace Zombie.SceneFlow
{
    public class GameplaySceneMain : QuantumCallbacks
    {
        [SerializeField] private CameraSetup cameraSetup;

        private void Awake()
        {
            PlayerSetup.OnPlayerInitialized += InitializeCamera;
        }

        private void OnDestroy()
        {
            PlayerSetup.OnPlayerInitialized -= InitializeCamera;
        }

        private void InitializeCamera(PlayerSetup playerSetup)
        {
            cameraSetup.Initialize(playerSetup.LookDirection);
        }
    }
}
