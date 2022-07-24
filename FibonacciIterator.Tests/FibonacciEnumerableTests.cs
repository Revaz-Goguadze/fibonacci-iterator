using System.Collections;
using NUnit.Framework;

namespace FibonacciIterator.Tests
{
    [TestFixture]
    public class FibonacciEnumerableTests
    {
        private static readonly object[][] FibonacciEnumeratorCountData =
        {
            new object[]
            {
                0,
                Array.Empty<int>(),
            },
            new object[]
            {
                1,
                new int[] { 0 },
            },
            new object[]
            {
                2,
                new int[] { 0, 1 },
            },
            new object[]
            {
                3,
                new int[] { 0, 1, 1 },
            },
            new object[]
            {
                4,
                new int[] { 0, 1, 1, 2 },
            },
            new object[]
            {
                5,
                new int[] { 0, 1, 1, 2, 3 },
            },
            new object[]
            {
                15,
                new int[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377 },
            },
            new object[]
            {
                20,
                new int[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181 },
            },
            new object[]
            {
                40,
                new int[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946, 17711, 28657, 46368, 75025, 121393, 196418, 317811, 514229, 832040, 1346269, 2178309, 3524578, 5702887, 9227465, 14930352, 24157817, 39088169, 63245986 },
            },
            new object[]
            {
                47,
                new int[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181, 6765, 10946, 17711, 28657, 46368, 75025, 121393, 196418, 317811, 514229, 832040, 1346269, 2178309, 3524578, 5702887, 9227465, 14930352, 24157817, 39088169, 63245986, 102334155, 165580141, 267914296, 433494437, 701408733, 1134903170, 1836311903 },
            },
        };

        private static readonly object[][] FibonacciEnumeratorCountAndSkipCountData =
        {
            new object[]
            {
                0,
                0,
                Array.Empty<int>(),
            },
            new object[]
            {
                1,
                0,
                new int[] { 0 },
            },
            new object[]
            {
                1,
                1,
                Array.Empty<int>(),
            },
            new object[]
            {
                2,
                0,
                new int[] { 0, 1 },
            },
            new object[]
            {
                2,
                1,
                new int[] { 1 },
            },
            new object[]
            {
                3,
                0,
                new int[] { 0, 1, 1 },
            },
            new object[]
            {
                3,
                1,
                new int[] { 1, 1 },
            },
            new object[]
            {
                3,
                2,
                new int[] { 1 },
            },
            new object[]
            {
                4,
                0,
                new int[] { 0, 1, 1, 2 },
            },
            new object[]
            {
                4,
                1,
                new int[] { 1, 1, 2 },
            },
            new object[]
            {
                4,
                2,
                new int[] { 1, 2 },
            },
            new object[]
            {
                4,
                3,
                new int[] { 2 },
            },
            new object[]
            {
                5,
                0,
                new int[] { 0, 1, 1, 2, 3 },
            },
            new object[]
            {
                15,
                0,
                new int[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377 },
            },
            new object[]
            {
                15,
                5,
                new int[] { 5, 8, 13, 21, 34, 55, 89, 144, 233, 377 },
            },
            new object[]
            {
                15,
                10,
                new int[] { 55, 89, 144, 233, 377 },
            },
            new object[]
            {
                15,
                15,
                Array.Empty<int>(),
            },
            new object[]
            {
                20,
                0,
                new int[] { 0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181 },
            },
            new object[]
            {
                20,
                10,
                new int[] { 55, 89, 144, 233, 377, 610, 987, 1597, 2584, 4181 },
            },
            new object[]
            {
                20,
                15,
                new int[] { 610, 987, 1597, 2584, 4181 },
            },
            new object[]
            {
                20,
                20,
                Array.Empty<int>(),
            },
        };

        [Test]
        public void FibonacciEnumerable_CountIsLessZero_ThrowsArgumentOutOfRangeException()
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                try
                {
                    _ = new FibonacciEnumerable(-1);
                }
                catch (ArgumentOutOfRangeException exception)
                {
                    Assert.That(exception.ParamName, Is.EqualTo("count"));
                    throw;
                }
            });
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(100)]
        public void FibonacciEnumerable_SkipCountIsLessZero_ThrowsArgumentOutOfRangeException(int count)
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                try
                {
                    _ = new FibonacciEnumerable(count, -1);
                }
                catch (ArgumentOutOfRangeException exception)
                {
                    Assert.That(exception.ParamName, Is.EqualTo("skipCount"));
                    throw;
                }
            });
        }

        [TestCaseSource(nameof(FibonacciEnumeratorCountData))]
        public void FibonacciEnumerable_ForEachWithCount_ReturnsElement(int count, int[] expectedResult)
        {
            // Arrange
            var actualResult = new List<int>();
            var fibonacciEnumerable = new FibonacciEnumerable(count: count);

            // Act
            foreach (int i in fibonacciEnumerable)
            {
                actualResult.Add(i);
            }

            // Assert
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            Assert.That(actualResult, Is.EquivalentTo(expectedResult));
        }

        [TestCaseSource(nameof(FibonacciEnumeratorCountAndSkipCountData))]
        public void FibonacciEnumerable_ForEachWithCountAndSkipCount_ReturnsElement(int count, int skipCount, int[] expectedResult)
        {
            // Arrange
            var actualResult = new List<int>();
            var fibonacciEnumerable = new FibonacciEnumerable(count: count, skipCount: skipCount);

            // Act
            foreach (int i in fibonacciEnumerable)
            {
                actualResult.Add(i);
            }

            // Assert
            Assert.AreEqual(expectedResult.Length, actualResult.Count);
            Assert.That(actualResult, Is.EquivalentTo(expectedResult));
        }

        [Test]
        public void FibonacciEnumerable_LastNumber_ReturnsLastNumber()
        {
            // Arrange
            int actualResult = -1;
            var fibonacciEnumerable = new FibonacciEnumerable();

            // Act
            foreach (int i in fibonacciEnumerable)
            {
                actualResult = i;
            }

            Assert.AreEqual(1836311903, actualResult);
        }

        [Test]
        public void GetEnumerator_ReturnsNewObject()
        {
            // Arrange
            var enumerable = new FibonacciEnumerable(100, 0);

            // Act
            var enumerator1 = enumerable.GetEnumerator();
            var enumerator2 = enumerable.GetEnumerator();
            var enumerator3 = (enumerable as IEnumerable).GetEnumerator();
            var enumerator4 = (enumerable as IEnumerable).GetEnumerator();

            // Assert
            Assert.That(enumerator1, Is.Not.SameAs(enumerator2));
            Assert.That(enumerator1, Is.Not.SameAs(enumerator3));
            Assert.That(enumerator1, Is.Not.SameAs(enumerator4));
            Assert.That(enumerator2, Is.Not.SameAs(enumerator3));
            Assert.That(enumerator2, Is.Not.SameAs(enumerator4));
            Assert.That(enumerator3, Is.Not.SameAs(enumerator4));
        }

        [Test]
        public void GetEnumerator_ReturnsFibonacciEnumerator()
        {
            // Arrange
            var enumerable = new FibonacciEnumerable(100, 0);

            // Act
            var enumerator1 = enumerable.GetEnumerator();
            var enumerator2 = (enumerable as IEnumerable).GetEnumerator();

            // Assert
            Assert.That(enumerator1, Is.TypeOf(typeof(FibonacciEnumerator)));
            Assert.That(enumerator2, Is.TypeOf(typeof(FibonacciEnumerator)));
        }

        [Test]
        public void FibonacciEnumerable_ImplementsIEnumerable()
        {
            // Assert
            Assert.IsTrue(typeof(IEnumerable<int>).IsAssignableFrom(typeof(FibonacciEnumerable)));
            Assert.IsTrue(typeof(IEnumerable).IsAssignableFrom(typeof(FibonacciEnumerable)));
        }
    }
}
