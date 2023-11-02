using Photon.Deterministic;

namespace Quantum.ZombieTest.Systems
{
    public unsafe class CharacterSystem : SystemMainThreadFilter<CharacterSystem.Filter>, ISignalOnPlayerDataSet
    {
        private CharacterMovement movement;
        
        public override void OnInit(Frame frame)
        {
            movement = new CharacterMovement();
        }

        public override void Update(Frame frame, ref Filter filter)
        {
            Input* playerInput = frame.GetPlayerInput(filter.PlayerLink->Player);
            
            FPVector3 absoluteMoveDirection = movement.GetMoveDirection(playerInput->Direction);
            FPVector3 relativeMoveDirection = movement.RotateMoveDirection(absoluteMoveDirection, filter.Transform->Forward);

            filter.Controller->Move(frame, filter.Entity, relativeMoveDirection);
            
            if (playerInput->LookDelta != FPVector2.Zero)
            {
                filter.PlayerLink->LookDirection = movement.RotateLookDirection(filter.PlayerLink->LookDirection, filter.Transform->Right, playerInput->LookDelta);
                FPVector3 forwardProjection = FPVector3.ProjectOnPlane(filter.PlayerLink->LookDirection, FPVector3.Up);
                
                frame.Signals.ChangeLookDirection(filter.Entity, filter.PlayerLink->LookDirection);
                frame.Events.OnLookDirectionChanged(filter.Entity, filter.PlayerLink->LookDirection);
                filter.Transform->Rotation = FPQuaternion.LookRotation(forwardProjection);
            }
            
            if (playerInput->Jump)
                filter.Controller->Jump(frame);
            else if(!filter.Controller->Grounded && filter.Controller->Velocity.Y > 0)
                filter.Controller->Velocity.Y = 0;
        }
        
        public void OnPlayerDataSet(Frame frame, PlayerRef player)
        {
            RuntimePlayer playerData = frame.GetPlayerData(player);
            EntityPrototype entityPrototype = frame.FindAsset<EntityPrototype>(playerData.EntityPrototypeAsset.Id);

            EntityRef playerInstance = frame.Create(entityPrototype);

            if (frame.Unsafe.TryGetPointer(playerInstance, out PlayerLink* link))
            {
                link->Player = player;

                if (frame.Unsafe.TryGetPointer(playerInstance, out Transform3D* transform))
                {
                    transform->Position.X = 0 + player;
                    link->LookDirection = transform->Forward;
                }
            }
        }
        
        public struct Filter
        {
            public EntityRef Entity;
            public CharacterController3D* Controller;
            public Transform3D* Transform;
            public PlayerLink* PlayerLink;
        }
    }
}