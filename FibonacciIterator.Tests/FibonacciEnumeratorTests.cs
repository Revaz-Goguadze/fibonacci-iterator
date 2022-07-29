using System.Collections;
using System.Reflection;
using NUnit.Framework;

namespace FibonacciIterator.Tests
{
    [TestFixture]
    public class FibonacciEnumeratorTests
    {
        [Test]
        public void Current_NoMoveNextCalls_ThrowsInvalidOperationException()
        {
            // Arrange
            IEnumerator<int> fibonacciEnumerator = new FibonacciEnumerable(100, 0).GetEnumerator();

            // Assert
            Assert.Throws<InvalidOperationException>(() => _ = fibonacciEnumerator.Current);
        }

        [TestCase(10)]
        public void Current_AfterResetCall_ThrowsInvalidOperationException(int iterations)
        {
            // Arrange
            IEnumerator<int> fibonacciEnumerator = new FibonacciEnumerable(100, 0).GetEnumerator();

            while (iterations-- > 0)
            {
                fibonacciEnumerator.MoveNext();
            }

            fibonacciEnumerator.Reset();

            // Assert
            Assert.Throws<InvalidOperationException>(() => _ = fibonacciEnumerator.Current);
        }

        [TestCase(0, 0, 0, ExpectedResult = false)]
        [TestCase(0, 1, 0, ExpectedResult = false)]
        [TestCase(1, 0, 0, ExpectedResult = true)]
        [TestCase(1, 0, 1, ExpectedResult = false)]
        [TestCase(1, 1, 0, ExpectedResult = false)]
        [TestCase(1, 2, 0, ExpectedResult = false)]
        [TestCase(2, 0, 0, ExpectedResult = true)]
        [TestCase(2, 0, 1, ExpectedResult = true)]
        [TestCase(2, 0, 2, ExpectedResult = false)]
        [TestCase(2, 1, 0, ExpectedResult = true)]
        [TestCase(2, 1, 1, ExpectedResult = false)]
        [TestCase(2, 2, 0, ExpectedResult = false)]
        [TestCase(2, 3, 0, ExpectedResult = false)]
        [TestCase(10, 0, 0, ExpectedResult = true)]
        [TestCase(10, 0, 9, ExpectedResult = true)]
        [TestCase(10, 0, 10, ExpectedResult = false)]
        [TestCase(10, 0, 11, ExpectedResult = false)]
        [TestCase(20, 0, 0, ExpectedResult = true)]
        [TestCase(20, 0, 19, ExpectedResult = true)]
        [TestCase(20, 0, 20, ExpectedResult = false)]
        [TestCase(20, 0, 21, ExpectedResult = false)]
        public bool MoveNext_WithIterations_ReturnsBoolean(int count, int skipCount, int iterations)
        {
            // Arrange
            IEnumerator<int> fibonacciEnumerator = new FibonacciEnumerable(count, skipCount).GetEnumerator();

            while (iterations-- > 0)
            {
                fibonacciEnumerator.MoveNext();
            }

            // Act
            return fibonacciEnumerator.MoveNext();
        }

        [TestCase(1, 0, 1, ExpectedResult = 0)]
        [TestCase(2, 0, 1, ExpectedResult = 0)]
        [TestCase(2, 0, 2, ExpectedResult = 1)]
        [TestCase(3, 0, 1, ExpectedResult = 0)]
        [TestCase(3, 0, 2, ExpectedResult = 1)]
        [TestCase(3, 0, 3, ExpectedResult = 1)]
        [TestCase(4, 0, 1, ExpectedResult = 0)]
        [TestCase(4, 0, 2, ExpectedResult = 1)]
        [TestCase(4, 0, 3, ExpectedResult = 1)]
        [TestCase(4, 0, 4, ExpectedResult = 2)]
        [TestCase(5, 0, 1, ExpectedResult = 0)]
        [TestCase(5, 0, 2, ExpectedResult = 1)]
        [TestCase(5, 0, 3, ExpectedResult = 1)]
        [TestCase(5, 0, 4, ExpectedResult = 2)]
        [TestCase(5, 0, 5, ExpectedResult = 3)]
        [TestCase(10, 0, 10, ExpectedResult = 34)]
        [TestCase(15, 0, 10, ExpectedResult = 34)]
        [TestCase(15, 0, 15, ExpectedResult = 377)]
        [TestCase(20, 0, 10, ExpectedResult = 34)]
        [TestCase(20, 0, 15, ExpectedResult = 377)]
        [TestCase(20, 0, 20, ExpectedResult = 4181)]
        public int Current_WithMoveNextIterations_ReturnsInteger(int count, int skipCount, int iterations)
        {
            // Arrange
            IEnumerator<int> fibonacciEnumerator = new FibonacciEnumerable(count, skipCount).GetEnumerator();

            while (iterations-- > 0)
            {
                fibonacciEnumerator.MoveNext();
            }

            // Act
            return fibonacciEnumerator.Current;
        }

        [TestCase(10, 0, 1, 1, ExpectedResult = 0)]
        [TestCase(10, 4, 1, 1, ExpectedResult = 3)]
        [TestCase(10, 0, 1, 1, ExpectedResult = 0)]
        [TestCase(10, 0, 10, 1, ExpectedResult = 0)]
        [TestCase(10, 0, 10, 10, ExpectedResult = 34)]
        [TestCase(10, 9, 1, 1, ExpectedResult = 34)]
        [TestCase(20, 0, 1, 1, ExpectedResult = 0)]
        [TestCase(20, 0, 20, 1, ExpectedResult = 0)]
        [TestCase(20, 0, 20, 10, ExpectedResult = 34)]
        [TestCase(20, 0, 20, 20, ExpectedResult = 4181)]
        [TestCase(20, 19, 1, 1, ExpectedResult = 4181)]
        public int Current_AfterResetAndMoveNextCalls_ReturnsInteger(int count, int skipCount, int iterations, int iterationsAfterReset)
        {
            // Arrange
            IEnumerator<int> fibonacciEnumerator = new FibonacciEnumerable(count, skipCount).GetEnumerator();

            while (iterations-- > 0)
            {
                fibonacciEnumerator.MoveNext();
            }

            fibonacciEnumerator.Reset();

            while (iterationsAfterReset-- > 0)
            {
                fibonacciEnumerator.MoveNext();
            }

            // Act
            return fibonacciEnumerator.Current;
        }

        [Test]
        public void FibonacciEnumerator_ImplementsIEnumerator()
        {
            // Assert
            Assert.IsTrue(typeof(IEnumerator<int>).IsAssignableFrom(typeof(FibonacciEnumerator)));
            Assert.IsTrue(typeof(IEnumerator).IsAssignableFrom(typeof(FibonacciEnumerator)));
        }

        [Test]
        public void FibonacciEnumerator_HasOnlyIntegerFields()
        {
            // Arrange
            FieldInfo[] fields = typeof(FibonacciEnumerator).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.NonPublic);

            // Assert
            foreach (FieldInfo field in fields)
            {
                Assert.That(field.FieldType, Is.EqualTo(typeof(int)));
            }
        }

        [Test]
        public void FibonacciEnumerator_HasOnlyPrivateFields()
        {
            // Arrange
            FieldInfo[] fields = typeof(FibonacciEnumerator).GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);

            // Assert
            Assert.That(fields.Length, Is.EqualTo(0));
        }
    }
}
