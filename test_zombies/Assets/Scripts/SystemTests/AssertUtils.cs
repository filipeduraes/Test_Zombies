using NUnit.Framework;

namespace Zombie.Utils.TestUtils
{
    public static class AssertUtils
    {
        public static void IsAlmostEqualTo(float value, float expected, float error = 0.0001f)
        {
            Assert.That(value, Is.InRange(expected - error, expected + error));
        } 
    }
}