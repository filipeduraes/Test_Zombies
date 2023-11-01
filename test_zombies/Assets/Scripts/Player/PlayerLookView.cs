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
            Vector3 direction = callback.direction.ToUnityVector3();

            if (Vector3.Dot(direction, transform.parent.forward) > 0.05f) 
                transform.forward = direction;
        }
    }
}