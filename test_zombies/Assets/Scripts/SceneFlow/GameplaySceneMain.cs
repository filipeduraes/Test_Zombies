using Quantum;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zombie.Camera;
using Zombie.Player;

namespace Zombie.SceneFlow
{
    public class GameplaySceneMain : QuantumCallbacks
    {
        [SerializeField] private CameraSetup cameraSetup;
        [SerializeField] private int menuSceneIndex;
        [SerializeField] private float menuDelay = 2f;
        
        private DispatcherSubscription finishListener;

        private void Awake()
        {
            PlayerSetup.OnLocalPlayerInitialized += InitializeCamera;
            PlayerDamageSender.OnDeath += ReturnToMenuInSeconds;
            finishListener = QuantumEvent.Subscribe<EventOnTimerFinished>(this, FinishGame);
        }

        private void OnDestroy()
        {
            PlayerSetup.OnLocalPlayerInitialized -= InitializeCamera;
            PlayerDamageSender.OnDeath -= ReturnToMenuInSeconds;
            QuantumEvent.Unsubscribe(finishListener);
        }

        private void InitializeCamera(PlayerSetup playerSetup)
        {
            cameraSetup.Initialize(playerSetup.LookDirection);
        }
        
        private void FinishGame(EventOnTimerFinished callback)
        {
            QuantumRunner.ShutdownAll(true);
            ReturnToMenuInSeconds();
        }
        
        private void ReturnToMenuInSeconds()
        {
            Invoke(nameof(ReturnToMenu), menuDelay);
        }

        private void ReturnToMenu()
        {
            SceneManager.LoadSceneAsync(menuSceneIndex);
        }
    }
}
