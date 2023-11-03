using System;
using Photon.Deterministic;
using Quantum;
using UnityEngine;

namespace Zombie.Player
{
    public class PlayerLookView : MonoBehaviour
    {
        private EntityRef playerEntity;
        private DispatcherSubscription listener;

        public void Initialize(EntityRef playerEntity)
        {
            this.playerEntity = playerEntity;
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