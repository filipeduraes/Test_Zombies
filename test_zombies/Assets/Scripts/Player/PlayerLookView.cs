using Quantum;
using UnityEngine;

namespace Zombie.Player
{
    public class PlayerLookView : MonoBehaviour
    {
        [SerializeField] private EntityView view;
        
        private EntityRef playerEntity;
        private DispatcherSubscription listener;

        public void Initialize()
        {
            playerEntity = view.EntityRef;
        }

        private void OnEnable()
        {
            listener = QuantumEvent.Subscribe<EventOnLookDirectionChanged>(this, LookDirectionChanged);
        }

        private void OnDisable()
        {
            QuantumEvent.UnsubscribeListener(listener);
        }

        private void LookDirectionChanged(EventOnLookDirectionChanged callback)
        {
            if(callback.sender != playerEntity)
                return;
            
            Vector3 directionVector = callback.direction.ToUnityVector3();

            if (Vector3.Dot(directionVector, transform.parent.forward) > 0.05f)
                transform.forward = directionVector;
        }
    }
}