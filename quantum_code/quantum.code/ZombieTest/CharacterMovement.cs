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
    }
}