using System;
using Quantum;
using UnityEngine;

namespace Zombie.Player
{
    public class PlayerDamageSender : MonoBehaviour
    {
        public static event Action<float> OnDamage = delegate { };
        public static event Action OnDeath = delegate { };
        private EntityRef playerEntity;
        private DispatcherSubscription damageListener;
        
        public void Initialize(EntityRef entity)
        {
            playerEntity = entity;
            damageListener = QuantumEvent.Subscribe<EventOnDamage>(this, SendDamage);
        }

        private void OnDestroy()
        {
            QuantumEvent.Unsubscribe(damageListener);
        }

        private void SendDamage(EventOnDamage callback)
        {
            if (callback.target == playerEntity)
            {
                float t = (float)callback.targetDamageable.CurrentHealth / callback.targetDamageable.MaxHealth;
                OnDamage(t);

                if (callback.targetDamageable.CurrentHealth <= 0)
                    OnDeath();
            }
        }
    }
}