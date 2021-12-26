# Task Examples
Task benchmark for pre-computed/trivially computed data
#### `Task.FromResult`,`Task.Run` and `ValueTask` benchmark comparation for pre-computed or trivially computed data

:-1: `Task.Run` 


For pre-computed results, there's no need to call `Task.Run`, that will end up queuing a work item to the thread pool that will immediately complete with the pre-computed value. Instead, use `Task.FromResult`, to create a task wrapping already computed data.

This example wastes a thread-pool thread to return a trivially computed value.

```C#
public class Tasks
{
   public Task<int> CalcAsync(int a, int b)
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
   public Task<int> CalcAsync(int a, int b)
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
   public ValueTask<int> CalcAsync(int a, int b)
   {
       return new ValueTask<int>(a * b);
   }
}
```
## Benchmark results

A benchmark is a set of measurements related to the performance of a piece of code in an application and can helps you to identify portions of code that need be refactoring. 
Is essential  understanding the performance metrics of the methods in your application and it is always a good approach to have the metrics at hand when you’re optimizing code. Also it is very important for us to know if the changes made in the code has improved or worsened the performance. 

![image](https://user-images.githubusercontent.com/8992182/147397635-15d874fa-7195-4689-a035-9c18edbd2a18.png)




