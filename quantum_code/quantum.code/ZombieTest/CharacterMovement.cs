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
    }
}