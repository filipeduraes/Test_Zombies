using Photon.Deterministic;

namespace Quantum.ZombieTest.Systems
{
    public unsafe class CharacterSystem : SystemMainThreadFilter<CharacterSystem.Filter>
    {
        private CharacterMovement movement;

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