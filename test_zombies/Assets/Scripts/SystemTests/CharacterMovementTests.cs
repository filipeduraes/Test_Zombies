using NUnit.Framework;
using Photon.Deterministic;
using Quantum.ZombieTest;
using Zombie.Utils.TestUtils;
using Assert = NUnit.Framework.Assert;

namespace Zombie.SystemTests
{
    [TestFixture]
    public class CharacterMovementTests
    {
        private CharacterMovement movement;
        
        [SetUp]
        public void Setup()
        {
            FPLut.Init(@"Assets\Plugins\Photon\Quantum\Resources\LUT");
            movement = new CharacterMovement();
        }
        
        [Test]
        public void Input_Gets_Converted_To_Axis_Direction()
        {
            FPVector3 direction = movement.GetMoveDirection(new FPVector2(1, 0));
            Assert.That(direction, Is.EqualTo(new FPVector3(1, 0, 0)));
            
            direction = movement.GetMoveDirection(new FPVector2(0, 1));
            Assert.That(direction, Is.EqualTo(new FPVector3(0, 0, 1)));
            
            direction = movement.GetMoveDirection(new FPVector2(1, 1));
            AssertUtils.IsAlmostEqualTo(direction.X.AsFloat, 0.707f);
            AssertUtils.IsAlmostEqualTo(direction.Y.AsFloat, 0f);
            AssertUtils.IsAlmostEqualTo(direction.Z.AsFloat, 0.707f);
        }

        [Test]
        public void Direction_Must_Be_Unit_Vector()
        {
            FPVector3 direction = movement.GetMoveDirection(new FPVector2(200, 200));
            Assert.That(direction.SqrMagnitude.AsDouble, Is.InRange(0.98, 1.01));
        }

        [Test]
        public void Direction_Gets_Rotated_To_Player_Direction()
        {
            FPVector3 moveDirection = movement.GetMoveDirection(new FPVector2(0, -1));
            FPVector3 playerForward = new FPVector3(0, 1, 1).Normalized;
            FPVector3 newMoveDirection = movement.RotateMoveDirection(moveDirection, playerForward);

            FPVector3 expectedDirection = -playerForward;
            AssertUtils.IsAlmostEqualTo(newMoveDirection.X.AsFloat, expectedDirection.X.AsFloat);
            AssertUtils.IsAlmostEqualTo(newMoveDirection.Y.AsFloat, expectedDirection.Y.AsFloat);
            AssertUtils.IsAlmostEqualTo(newMoveDirection.Z.AsFloat, expectedDirection.Z.AsFloat);
        }

        [Test]
        public void Look_Delta_Rotates_Direction_Accordingly()
        {
            FPVector3 originalDirection = FPVector3.Forward;

            FPVector3 lookDirection = movement.RotateLookDirection(originalDirection, new FPVector2(1, 0));
            FP dot = FPVector3.Dot(FPVector3.Right, lookDirection);
            Assert.That(dot.AsFloat, Is.GreaterThan(0.05f));
            
            lookDirection = movement.RotateLookDirection(originalDirection, new FPVector2(-1, 0));
            dot = FPVector3.Dot(FPVector3.Right, lookDirection);
            Assert.That(dot.AsFloat, Is.LessThan(-0.05f));
            
            lookDirection = movement.RotateLookDirection(originalDirection, new FPVector2(0, 1));
            dot = FPVector3.Dot(FPVector3.Up, lookDirection);
            Assert.That(dot.AsFloat, Is.GreaterThan(0.05f));
            
            lookDirection = movement.RotateLookDirection(originalDirection, new FPVector2(0, -1));
            dot = FPVector3.Dot(FPVector3.Up, lookDirection);
            Assert.That(dot.AsFloat, Is.LessThan(-0.05f));
        }
    }
}