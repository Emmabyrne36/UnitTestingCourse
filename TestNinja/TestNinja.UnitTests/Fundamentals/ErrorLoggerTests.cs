using NUnit.Framework;
using System;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class ErrorLoggerTests
    {
        [Test]
        public void Log_WhenCalled_SetTheLastErrorProperty()
        {
            ErrorLogger logger = new ErrorLogger();

            logger.Log("a");

            Assert.That(logger.LastError, Is.EqualTo("a"));
        }

        [Test]
        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        public void Log_InvalidError_ThrowArgumentNullException(string error)
        {
            ErrorLogger logger = new ErrorLogger();

            // To test a method that throws an exception, use a delegate
            Assert.That(() => logger.Log(error), Throws.ArgumentNullException);
        }

        [Test]
        public void Log_ValidError_RaiseErrorLoggedEvent()
        {
            ErrorLogger logger = new ErrorLogger();

            // Subscribe to event before acting
            var id = Guid.Empty;
            logger.ErrorLogged += (sender, args) => { id = args; };

            logger.Log("a");

            Assert.That(id, Is.Not.EqualTo(Guid.Empty));
        }
    }
}
