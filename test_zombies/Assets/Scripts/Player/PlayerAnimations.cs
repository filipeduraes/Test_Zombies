using System;
using Quantum;
using UnityEngine;

namespace Zombie.Player.Animations
{
    public unsafe class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private EntityView view;
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private AnimatorParameter walkVelocityKey;
        [SerializeField] private AnimatorParameter walkDirectionKey;
        [SerializeField] private AnimatorParameter jumpDirectionKey;
        [SerializeField] private AnimatorParameter isGroundedKey;
        [SerializeField] private AnimatorParameter damageKey;
        
        private CharacterController3D* controller;
        private Transform3D* playerTransform;
        private DispatcherSubscription damageListener;
        private EntityRef entity;
        private float walkDirectionDot;
        private float walkVelocity;

        public void Initialize(Frame frame)
        {
            entity = view.EntityRef;

            if (frame.Unsafe.TryGetPointer(entity, out CharacterController3D* playerController))
            {
                controller = playerController;
            }
            
            if (frame.Unsafe.TryGetPointer(entity, out Transform3D* playerTransform3D))
            {
                playerTransform = playerTransform3D;
            }

            damageListener = QuantumEvent.Subscribe<EventOnDamage>(this, TriggerDamage);

            walkVelocityKey.CacheHash();
            walkDirectionKey.CacheHash();
            jumpDirectionKey.CacheHash();
            isGroundedKey.CacheHash();
            damageKey.CacheHash();
        }

        private void OnDestroy()
        {
            QuantumEvent.UnsubscribeListener<EventOnDamage>(damageListener);
        }

        private void FixedUpdate()
        {
            Vector3 velocity = controller->Velocity.ToUnityVector3();
            velocity.y = 0;
            walkVelocity = Mathf.Round(velocity.sqrMagnitude);
            
            walkDirectionDot = Vector3.Dot(playerTransform->Forward.ToUnityVector3(), velocity.normalized);
            walkDirectionDot = Mathf.Round(walkDirectionDot);
        }

        private void Update()
        {
            playerAnimator.SetFloat(walkVelocityKey, walkVelocity);
            playerAnimator.SetFloat(walkDirectionKey, Mathf.Sign(walkDirectionDot));
            playerAnimator.SetFloat(jumpDirectionKey, Mathf.Sign(controller->Velocity.Y.AsFloat));
            playerAnimator.SetBool(isGroundedKey, controller->Grounded);
        }
        
        private void TriggerDamage(EventOnDamage callback)
        {
            if(callback.target == entity)
                playerAnimator.SetTrigger(damageKey);
        }
    }
}