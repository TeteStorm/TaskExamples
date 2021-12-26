# Task Examples
Task benchmark for pre-computed/trivially computed data
## `Task.FromResult`,`Task.Run`, `ValueTask` benchmark comparation for pre-computed or trivially computed data

:-1: `Task.Run` 


For pre-computed results, there's no need to call `Task.Run`, that will end up queuing a work item to the thread pool that will immediately complete with the pre-computed value. Instead, use `Task.FromResult`, to create a task wrapping already computed data.

This example wastes a thread-pool thread to return a trivially computed value.

```C#
public class Tasks
{
   public Task<int> AddAsync(int a, int b)
   {
       return Task.Run(() => a * b);
   }
}
```

:+1: `Task.FromResult`

This example uses `Task.FromResult` to return the trivially computed value. It does not use any extra threads as a result but remain result in a `Task` allocation.

```C#
public class Tasks
{
   public Task<int> AddAsync(int a, int b)
   {
       return Task.FromResult(a * b);
   }
}
```

:clap: `ValueTask`

It does not use any extra threads as a result and also does not allocate an object on the managed heap.

```C#
public class Tasks
{
   public ValueTask<int> AddAsync(int a, int b)
   {
       return new ValueTask<int>(a * b);
   }
}
```



``` ini

BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19043.1415 (21H1/May2021Update)
Intel Core i7-7500U CPU 2.70GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK=5.0.404
  [Host]     : .NET 5.0.13 (5.0.1321.56516), X64 RyuJIT  [AttachedDebugger]
  DefaultJob : .NET 5.0.13 (5.0.1321.56516), X64 RyuJIT


```
|                 Method | Number |       Mean |     Error |    StdDev |  Gen 0 | Allocated |
|----------------------- |------- |-----------:|----------:|----------:|-------:|----------:|
|           AddAsyncTask |     10 | 815.937 ns | 8.0298 ns | 7.5111 ns | 0.0639 |     136 B |
| AddAsyncTaskFromResult |     10 |   8.337 ns | 0.2016 ns | 0.4340 ns | 0.0344 |      72 B |
|      AddAsyncValueTask |     10 |   9.734 ns | 0.1344 ns | 0.1257 ns |      - |         - |


