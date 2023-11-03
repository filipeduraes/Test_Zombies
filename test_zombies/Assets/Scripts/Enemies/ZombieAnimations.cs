using Quantum;
using UnityEngine;
using Zombie.Player.Animations;

namespace Zombie.Enemies
{
    public class ZombieAnimations : MonoBehaviour
    {
        [SerializeField] private EntityView zombieView;
        [SerializeField] private Animator animator;
        [SerializeField] private AnimatorParameter isMoving = "IsMoving";
        [SerializeField] private AnimatorParameter attack = "Attack";
        [SerializeField] private AnimatorParameter damageTrigger = "Damage";
        
        private DispatcherSubscription aiMovementListener;
        private DispatcherSubscription aiStoppedListener;
        private DispatcherSubscription aiAttackListener;
        private DispatcherSubscription aiDamageListener;

        private void Awake()
        {
            isMoving.CacheHash();
            attack.CacheHash();
            damageTrigger.CacheHash();
        }

        private void OnEnable()
        {
            aiMovementListener = QuantumEvent.Subscribe<EventOnAIMovement>(this, SetAIMovement);
            aiStoppedListener = QuantumEvent.Subscribe<EventOnAIStopped>(this, SetAIStopped);
            aiAttackListener = QuantumEvent.Subscribe<EventOnAIAttack>(this, TriggerAIAttack);
            aiDamageListener = QuantumEvent.Subscribe<EventOnDamage>(this, TriggerDamage);
        }

        private void OnDisable()
        {
            QuantumEvent.UnsubscribeListener<EventOnAIMovement>(aiMovementListener);
            QuantumEvent.UnsubscribeListener<EventOnAIStopped>(aiStoppedListener);
            QuantumEvent.UnsubscribeListener<EventOnAIAttack>(aiAttackListener);
            QuantumEvent.UnsubscribeListener<EventOnDamage>(aiDamageListener);
        }

        private void TriggerDamage(EventOnDamage callback)
        {
            if (callback.target == zombieView.EntityRef)
                animator.SetTrigger(damageTrigger);
        }
        
        private void TriggerAIAttack(EventOnAIAttack callback)
        {
            if(callback.entity == zombieView.EntityRef)
                animator.SetTrigger(attack);
        }

        private void SetAIStopped(EventOnAIStopped callback)
        {
            if(callback.entity == zombieView.EntityRef)
                animator.SetBool(isMoving, false);
        }

        private void SetAIMovement(EventOnAIMovement callback)
        {
            if(callback.entity == zombieView.EntityRef)
                animator.SetBool(isMoving, true);
        }
    }
}
