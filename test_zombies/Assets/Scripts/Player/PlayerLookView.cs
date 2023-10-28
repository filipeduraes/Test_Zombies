using Quantum;
using UnityEngine;

namespace Zombie.Player
{
    public class PlayerLookView : MonoBehaviour
    {
        private DispatcherSubscription listener;
        
        private void OnEnable()
        {
            listener = QuantumEvent.Subscribe<EventLookDirectionChanged>(this, UpdateLookDirection);
        }

        private void OnDisable()
        {
            QuantumEvent.UnsubscribeListener<EventLookDirectionChanged>(listener);
        }

        private void UpdateLookDirection(EventLookDirectionChanged callback)
        {
            transform.localPosition = -callback.direction.ToUnityVector3(); 
            transform.forward = callback.direction.ToUnityVector3();
        }
    }
}