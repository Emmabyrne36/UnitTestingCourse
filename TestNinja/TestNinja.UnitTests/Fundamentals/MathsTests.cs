using NUnit.Framework;
using System.Linq;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class MathsTests
    {
        private Math _maths;

        [SetUp]
        public void SetUp()
        {
            _maths = new Math();
        }


        [Test]
        public void Add_WhenCalled_ReturnsTheSumOfArguments()
        {
            var result = _maths.Add(1, 2);

            Assert.That(result, Is.EqualTo(3));
        }

        [Test]
        [TestCase(2,1,2)]
        [TestCase(1,2,2)]
        [TestCase(1,1,1)]
        public void Max_WhenCalled_ReturnsTheGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _maths.Max(a, b);

            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void GetOddNumbers_LimitIsGreaterThanZero_ReturnsOddNumbersUpToLimit()
        {
            var result = _maths.GetOddNumbers(5);

            // Assert.That(result, Is.Not.Empty);

            // Assert.That(result.Count(), Is.EqualTo(3));

            // Assert.That(result, Does.Contain(1));
            // Assert.That(result, Does.Contain(3));
            // Assert.That(result, Does.Contain(5));

            Assert.That(result, Is.EquivalentTo(new[] { 1, 3, 5 }));

            // Handy other Assertions to know
            // Assert.That(result, Is.Ordered);
            // Assert.That(result, Is.Unique);
        }
    }
}
