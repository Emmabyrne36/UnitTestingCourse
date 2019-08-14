using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class FizzBuzzTests
    {

        [Test]
        public void GetOutput_InputDivisibleBy3And5_ReturnsFizzBuzz()
        {
            string result = FizzBuzz.GetOutput(15);

            Assert.That(result, Is.EqualTo("FizzBuzz"));
        }

        [Test]
        public void GetOutput_InputDivisibleBy3Only_ReturnsFizz()
        {
            string result = FizzBuzz.GetOutput(3);

            Assert.That(result, Is.EqualTo("Fizz"));
        }

        [Test]
        public void GetOutput_InputDivisibleBy5Only_ReturnsBuzz()
        {
            string result = FizzBuzz.GetOutput(5);

            Assert.That(result, Is.EqualTo("Buzz"));
        }

        [Test]
        public void GetOutput_InputNotDivisibleBy3Or5_ReturnsInputtedNumber()
        {
            string result = FizzBuzz.GetOutput(1);

            Assert.That(result, Is.EqualTo("1"));
        }
    }
}
