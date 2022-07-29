# Fibonacci Iterator

Intermediate level task to practice explicit interface implementation and to implement the `IEnumerable<T>` and `IEnumerator<T>` interfaces for creating an iterator.

Estimated time to complete the task - 1h.

The task requires .NET 6 SDK installed.


## Task Description

In this task you have to [create an iterator](https://docs.microsoft.com/en-us/dotnet/csharp/iterators) to iterate over numbers in the [Fibonacci sequence](https://www.google.com/search?q=fibonacci+sequence).

The iterator should be implemented using the [IEnumerable&lt;T&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerable-1) and  [IEnumerator&lt;T&gt;](https://docs.microsoft.com/en-us/dotnet/api/system.collections.generic.ienumerator-1) [interfaces](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/interface), so the [foreach statement](https://docs.microsoft.com/en-us/dotnet/csharp/iterators#deeper-dive-into-foreach) can be used to iterate across all elements of the sequence.

```cs
var fibonacciEnumerable = new FibonacciEnumerable();

foreach (int number in fibonacciEnumerable)
{
}
```

The `FibonacciEnumerable` class has a constructor with two [optional arguments](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/named-and-optional-arguments#optional-arguments) - `count` and `skipCount`. Each argument has a default value.

```cs
public FibonacciEnumerable(int count = int.MaxValue, int skipCount = 0) { ... }
```

The `count` parameter contains the number of elements that the iterator produces.

```cs
var fibonacciEnumerable = new FibonacciEnumerable(5); // produces { 0, 1, 1, 2, 3 } numbers
```

The `skipCount` parameter contains the number of elements to skip before an iterator will return the first sequence element.

```cs
var fibonacciEnumerable = new FibonacciEnumerable(10, 5); // produces { 5, 8, 13, 21, 34 } numbers, the first five elements are skipped
```

The iterator consists of two classes - `FibonacciEnumerable` and `FibonacciEnumerator`.

* Implement the `IEnumerable<T>` interface in the [FibonacciEnumerable](FibonacciIterator/FibonacciEnumerable.cs#L6) class to define an enumerable object.
* Implement the `IEnumerator<T>` interface in the [FibonacciEnumerator](FibonacciIterator/FibonacciEnumerator.cs#L6) class to define an enumerator. Some of the interface members should be [implemented explicitly](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/interfaces/explicit-interface-implementation).

If you are coding in the Visual Studio, you may use the [Implement Interface](https://docs.microsoft.com/en-us/visualstudio/ide/reference/implement-interface) refactoring to enhance your experience with declaring an interface methods in a class.

The `FibonacciEnumerator` class should have only `int` private fields.