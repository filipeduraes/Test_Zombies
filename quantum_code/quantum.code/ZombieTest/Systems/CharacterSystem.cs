using Photon.Deterministic;

namespace Quantum.ZombieTest.Systems
{
    public unsafe class CharacterSystem : SystemMainThreadFilter<CharacterSystem.Filter>
    {
        private CharacterMovement movement;
        private FPVector3 lookDirection = FPVector3.Forward;
        
        public override void OnInit(Frame frame)
        {
            movement = new CharacterMovement();
        }

        public override void Update(Frame frame, ref Filter filter)
        {
            Input* playerInput = frame.GetPlayerInput(0);
            FPVector3 absoluteMoveDirection = movement.GetMoveDirection(playerInput->Direction);
            FPVector3 relativeMoveDirection = movement.RotateMoveDirection(absoluteMoveDirection, filter.Transform->Forward);

            filter.Controller->Move(frame, filter.Entity, relativeMoveDirection);
            
            if (playerInput->LookDelta != FPVector2.Zero)
            {
                lookDirection = movement.RotateLookDirection(lookDirection, filter.Transform->Right, playerInput->LookDelta);
                FPVector3 forwardProjection = FPVector3.ProjectOnPlane(lookDirection, FPVector3.Up);
                
                frame.Events.LookDirectionChanged(lookDirection);
                filter.Transform->Rotation = FPQuaternion.LookRotation(forwardProjection);
            }
            
            if (playerInput->JumpButton)
                filter.Controller->Jump(frame);
        }
        
        public struct Filter
        {
            public EntityRef Entity;
            public CharacterController3D* Controller;
            public Transform3D* Transform;
        }
    }
}