using Photon.Deterministic;
using Quantum;
using UnityEngine;
using Zombie.Inputs;
using Input = Quantum.Input;

namespace Zombie.Player
{
    public class PlayerInputs : MonoBehaviour
    {
        private GameplayInputs.PlayerActions PlayerActions => inputs.Player;
        private GameplayInputs inputs;

        private void Awake()
        {
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

            Input input = callback.Input;
            input.Direction = direction.ToFPVector2();
            input.JumpButton = PlayerActions.Jump.IsPressed();

            callback.SetInput(input, DeterministicInputFlags.Repeatable);
        }
    }
}
