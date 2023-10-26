using Photon.Deterministic;

namespace Quantum.ZombieTest
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
            FPVector3 moveDirection = movement.GetMoveDirection(playerInput->Direction);

            filter.Controller->Move(frame, filter.Entity, moveDirection);
        }
        
        public struct Filter
        {
            public EntityRef Entity;
            public CharacterController3D* Controller;
            public Transform3D* Transform;
        }
    }
}