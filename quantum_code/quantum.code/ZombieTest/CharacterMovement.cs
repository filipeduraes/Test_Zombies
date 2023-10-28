using Photon.Deterministic;

namespace Quantum.ZombieTest
{
    public class CharacterMovement
    {
        public FPVector3 GetMoveDirection(FPVector2 input)
        {
            FPVector3 direction = input.XOY;
            
            if (direction.SqrMagnitude > 1)
                direction = direction.Normalized;
            
            return direction;
        }

        public FPVector3 RotateMoveDirection(FPVector3 direction, FPVector3 forward)
        {
            FPQuaternion rotationOffset = FPQuaternion.FromToRotation(FPVector3.Forward, forward);
            return rotationOffset * direction;
        }

        public FPVector3 RotateLookDirection(FPVector3 lookForward, FPVector3 lookRight, FPVector2 delta)
        {
            FPVector3 lookUp = FPVector3.Cross(lookForward, lookRight);
            FPQuaternion offsetXRotation = FPQuaternion.AngleAxis(delta.X, lookUp);
            FPQuaternion offsetYRotation = FPQuaternion.AngleAxis(delta.Y, lookRight);

            FPVector3 lookDirection = offsetXRotation * lookForward;
            FPVector3 lookProjection = FPVector3.ProjectOnPlane(lookForward, FPVector3.Up);
            
            if(FPVector3.Dot(offsetYRotation * lookForward, lookProjection).AsFloat > 0.05f)
                lookDirection = offsetYRotation * lookDirection;
            
            return lookDirection;
        }
    }
}