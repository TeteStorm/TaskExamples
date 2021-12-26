using BenchmarkDotNet.Running;

namespace TaskExamples
{
    class Program
    {
        public static void Main(string[] args)
        {
            BenchmarkRunner.Run<Tasks>();
        }

    }
}
