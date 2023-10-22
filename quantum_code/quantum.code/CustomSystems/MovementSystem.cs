using Photon.Deterministic;

namespace Quantum
{
    public unsafe struct  PlayerMovementFilter
    {
        public EntityRef EntityRef;
        public PlayerID* PlayerID;
        public Transform3D* Transform;
        public CharacterController3D* Kcc;
    }
    
    unsafe class MovementSystem : SystemMainThreadFilter<PlayerMovementFilter>
    {
        private static readonly FP ROTATION_SPEED_MULTIPLIER = FP._5;

        public override void Update(Frame f, ref PlayerMovementFilter filter)
        {
            var input = f.GetPlayerInput(filter.PlayerID->PlayerRef);
            
            if (input->MovementHorizontal == 0 &&
                input->MoveBack.WasPressed && input->MovementVertical < 0) {
                filter.Transform->Rotation *= FPQuaternion.AngleAxis( FP.Rad2Deg * FP.Rad_180, FPVector3.Up);
                filter.Kcc->Move(f, filter.EntityRef, FPVector3.Zero);
                return;
            }
            
            var inputVector = new FPVector3(input->MovementHorizontal, FP._0, input->MovementVertical);
            var movementVector = filter.Transform->Rotation * inputVector;
            
            var movementAcceleration = FPVector2.Dot(filter.Transform->Forward.XZ.Normalized, movementVector.XZ);
            var forwardVelocity = FPMath.Abs(movementAcceleration);

            var angle = FPVector2.Radians(filter.Transform->Forward.XZ.Normalized, movementVector.XZ);
            angle = angle > FP.Rad_90 ? (angle - FP.Pi) : angle;
            angle *= FP.Rad2Deg;
            angle = FPMath.Abs(angle);
            var rotationAcceleration = FPVector2.Dot(filter.Transform->Right.XZ.Normalized, movementVector.XZ);

            var rotation = angle * rotationAcceleration * ROTATION_SPEED_MULTIPLIER * f.DeltaTime;
            var viewOrientation = input->MovementHorizontal == FP._0 ?
                                    FPQuaternion.Identity :
                                    FPQuaternion.AngleAxis(rotation, FPVector3.Up);
            
            filter.Transform->Rotation *= viewOrientation;

            if (input->Jump.WasPressed)
            {
                f.Events.PlayerJump(filter.PlayerID->PlayerRef);
                filter.Kcc->Jump(f);
            }

            filter.Kcc->Move(f, filter.EntityRef, filter.Transform->Forward * forwardVelocity);
        }
    }
}
