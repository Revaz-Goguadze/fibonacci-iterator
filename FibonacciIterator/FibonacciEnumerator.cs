using System.Collections;

namespace FibonacciIterator
{
    /// <summary>
    /// Represents an enumerator object to iterate over the Fibonacci sequence numbers.
    /// </summary>
    public sealed class FibonacciEnumerator : IEnumerator, IEnumerator<int>
    {
        // TODO Implement an enumerator to iterate over the Fibonacci sequence numbers.
        private int count;
        private int skipCount;
        private int currentFib;
        private int previousFib;
        private int fibIndex;

        public FibonacciEnumerator(int count, int skipCount)
        {
            this.count = count;
            this.skipCount = skipCount;

            this.currentFib = 0;
            this.previousFib = 1;
            this.fibIndex = 0;

            // Skipping the specified number of elements
            for (int i = 0; i < skipCount; i++)
            {
                this.NextFibonacci();
            }
        }

        public int Current => this.currentFib;

        // ReSharper disable once HeapView.BoxingAllocation
        object IEnumerator.Current => this.Current;

        public void Dispose()
        {
            // No resources to dispose
        }

        public bool MoveNext()
        {
            if (this.fibIndex < this.count)
            {
                this.NextFibonacci();
                this.fibIndex++;
                return true;
            }

            return false;
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }

        private void NextFibonacci()
        {
            int temp = this.currentFib;
            this.currentFib += this.previousFib;
            this.previousFib = temp;
        }
    }
}
