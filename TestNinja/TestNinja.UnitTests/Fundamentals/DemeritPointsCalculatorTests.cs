using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class DemeritPointsCalculatorTests
    {
        private DemeritPointsCalculator _demeritPointsCalc;

        [SetUp]
        public void SetUp()
        {
            _demeritPointsCalc = new DemeritPointsCalculator();
        }

        [Test]
        [TestCase(-1)]
        [TestCase(500)]
        public void CalculateDemeritPoints_InvalidInput_ReturnsArgumentOutOfRangeException(int speed)
        {
            // To test a method that throws and exception, use a delegate
            Assert.That(() => _demeritPointsCalc.CalculateDemeritPoints(speed), Throws.TypeOf<ArgumentOutOfRangeException>());
        }

        [Test]
        [TestCase(0, 0)]
        [TestCase(60, 0)]
        [TestCase(65, 0)]
        [TestCase(66, 0)]
        [TestCase(70, 1)]
        [TestCase(75, 2)]
        [TestCase(80, 3)]
        public void CalculateDemeritPoints_WhenCalled_ReturnsDemeritPoints(int speed, int expectedPoints)
        {
            int result = _demeritPointsCalc.CalculateDemeritPoints(speed);

            Assert.That(result, Is.EqualTo(expectedPoints));
        }
    }
}
