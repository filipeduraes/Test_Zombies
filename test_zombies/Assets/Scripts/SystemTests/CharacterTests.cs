using NUnit.Framework;
using Photon.Deterministic;
using Quantum.ZombieTest;
using Assert = NUnit.Framework.Assert;

namespace Zombie.SystemTests
{
    [TestFixture]
    public class CharacterTests
    {
        [SetUp]
        public void Setup()
        {
            FPLut.Init(@"Assets\Plugins\Photon\Quantum\Resources\LUT");
        }
        
        [TestCase]
        public void Input_Gets_Converted_To_Axis_Direction()
        {
            CharacterMovement movement = new();
            
            FPVector3 direction = movement.GetMoveDirection(new FPVector2(1, 0));
            Assert.That(direction, Is.EqualTo(new FPVector3(1, 0, 0)));
            
            direction = movement.GetMoveDirection(new FPVector2(0, 1));
            Assert.That(direction, Is.EqualTo(new FPVector3(0, 0, 1)));
            
            direction = movement.GetMoveDirection(new FPVector2(1, 1));
            Assert.That(direction.X.AsDouble, Is.InRange(0.706, 0.708));
            Assert.That(direction.Y.AsDouble, Is.EqualTo(0));
            Assert.That(direction.Z.AsDouble, Is.InRange(0.706, 0.708));
        }

        [TestCase]
        public void Direction_Must_Be_Unit_Vector()
        {
            CharacterMovement movement = new();

            FPVector3 direction = movement.GetMoveDirection(new FPVector2(200, 200));
            Assert.That(direction.SqrMagnitude.AsDouble, Is.InRange(0.98, 1.01));
        }
    }
}