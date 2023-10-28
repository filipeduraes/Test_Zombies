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

        public FPVector3 RotateLookDirection(FPVector3 originalLookDirection, FPVector2 delta)
        {
            FPQuaternion offsetXRotation = FPQuaternion.AngleAxis(delta.X * 10, FPVector3.Up);
            //FPQuaternion offsetYRotation = FPQuaternion.AngleAxis(-delta.Y * 10, FPVector3.Right);
            
            FPVector3 lookDirection = offsetXRotation * originalLookDirection;
            //lookDirection = offsetYRotation * lookDirection;
            
            return lookDirection;
        }
    }
}