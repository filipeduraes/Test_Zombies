using Photon.Deterministic;
using Quantum;
using UnityEngine;
using Zombie.Inputs;
using Input = Quantum.Input;

namespace Zombie.Player
{
    public class PlayerInputs : MonoBehaviour
    {
        [SerializeField] private float snappiness = 10.0f;

        private GameplayInputs.PlayerActions PlayerActions => inputs.Player;
        private GameplayInputs inputs;
        private Vector2 lookVelocity;
        private Vector2 currentLookInput;
        private Vector2 accumulator = Vector2.zero;

        private void Awake()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            inputs = new GameplayInputs();
            inputs.Enable();
        }

        private void OnEnable()
        {
            QuantumCallback.Subscribe<CallbackPollInput>(this, CheckInput);
        }

        private void OnDisable()
        {
            QuantumCallback.UnsubscribeListener<CallbackPollInput>(this);
        }

        private void CheckInput(CallbackPollInput callback)
        {
            Vector2 direction = PlayerActions.Move.ReadValue<Vector2>();
            Vector2 lookDelta = PlayerActions.Camera.ReadValue<Vector2>() * 0.5f;

            accumulator = Vector2.Lerp(accumulator, lookDelta, snappiness * Time.deltaTime);

            Input input = callback.Input;
            input.Direction = direction.ToFPVector2();
            input.LookDelta = accumulator.ToFPVector2();
            input.JumpButton = PlayerActions.Jump.IsPressed();

            callback.SetInput(input, DeterministicInputFlags.Repeatable);
        }
    }
}