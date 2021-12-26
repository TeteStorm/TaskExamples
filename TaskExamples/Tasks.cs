using BenchmarkDotNet.Attributes;
using System.Threading.Tasks;

namespace TaskExamples
{
    [MemoryDiagnoser]
    public class Tasks
    {
        [Params(10)]
        public int Number { get; set; }

        [Benchmark]
        public Task<int> CalcAsyncTask()
        {
            return Task.Run(() => this.Number * this.Number);
        }

        [Benchmark]
        public Task<int> CalcAsyncTaskFromResult()
        {
            return Task.FromResult(this.Number * this.Number);
        }

        [Benchmark]
        public ValueTask<int> CalcAsyncValueTask()
        {
            return new ValueTask<int>(this.Number * this.Number);
        }
    }
}
