using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests
{
    [TestFixture]
    class StackTests
    {
        private Stack<object> _stack; // could change object to string or int etc.

        [SetUp]
        public void SetUp()
        {
            _stack = new Stack<object>();
        }

        [Test]
        public void Push_ValidArgument_AddObjectToStack()
        {
            int countBeforePush = _stack.Count;

            _stack.Push(1);

            Assert.That(_stack.Count, Is.GreaterThan(countBeforePush));
            Assert.That(_stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Push_ArgIsNull_ReturnsArgumentNullException()
        {
            Assert.That(() => _stack.Push(null), Throws.ArgumentNullException);
        }

        [Test]
        public void Count_EmptyStack_Returns0()
        {
            Assert.That(_stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pop_EmptyStack_ReturnsInvalidOperationException()
        {
            Assert.That(() => _stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_RemoveLastAddedItem_ReturnsLastItemAddedToStack()
        {
            // Arrange
            _stack.Push(1);

            // Act
            var result = _stack.Pop();

            // Assert
            Assert.That(result, Is.EqualTo(1));
            Assert.That(_stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pop_RemoveLastAddedItem_RemovesObjectOnTop()
        {
            // Arrange
            _stack.Push(1);
            _stack.Push(2);
            _stack.Push(3);

            // Act
            _stack.Pop();

            // Assert
            Assert.That(_stack.Count, Is.EqualTo(2));
        }

        [Test]
        public void Peek_EmptyStack_ReturnsInvalidOperationException()
        {
            Assert.That(() => _stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_PeekItem_ReturnsLastItemAdded()
        {
            _stack.Push(1);

            var result = _stack.Peek();

            Assert.That(result, Is.EqualTo(1));
            Assert.That(_stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Peek_PeekItem_DoesNotRemoveObjectFromTopOfStack()
        {
            _stack.Push(1);

            _stack.Peek();

            Assert.That(_stack.Count, Is.EqualTo(1));
        }
    }
}
