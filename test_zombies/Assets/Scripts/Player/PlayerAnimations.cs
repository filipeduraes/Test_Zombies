using Quantum;
using UnityEngine;

namespace Zombie.Player.Animations
{
    public unsafe class PlayerAnimations : MonoBehaviour
    {
        [SerializeField] private Animator playerAnimator;
        [SerializeField] private AnimatorParameter walkVelocityKey;
        [SerializeField] private AnimatorParameter walkDirectionKey;
        [SerializeField] private AnimatorParameter jumpDirectionKey;
        [SerializeField] private AnimatorParameter isGroundedKey;
        
        private CharacterController3D* controller;
        private Transform3D* playerTransform;
        private float walkDirectionDot;
        private float walkVelocity;

        public void Initialize(Frame frame, EntityRef playerEntity)
        {
            if (frame.Unsafe.TryGetPointer(playerEntity, out CharacterController3D* playerController))
            {
                controller = playerController;
            }
            
            if (frame.Unsafe.TryGetPointer(playerEntity, out Transform3D* playerTransform3D))
            {
                playerTransform = playerTransform3D;
            }
            
            walkVelocityKey.CacheHash();
            walkDirectionKey.CacheHash();
            jumpDirectionKey.CacheHash();
            isGroundedKey.CacheHash();
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
    }
}
