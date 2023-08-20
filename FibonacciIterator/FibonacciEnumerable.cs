using System.Collections;

namespace FibonacciIterator
{
    /// <summary>
    /// Represents an enumerable object to iterate over the Fibonacci sequence numbers.
    /// </summary>
    public sealed class FibonacciEnumerable : IEnumerable<int>
    {
        private readonly int count;
        private readonly int skipCount;

        public FibonacciEnumerable(int count = int.MaxValue, int skipCount = 0)
        {
            this.count = count;
            this.skipCount = skipCount;
        }

        public IEnumerator<int> GetEnumerator()
        {
            return new FibonacciEnumerator(this.count, this.skipCount);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
