using BenchmarkDotNet.Running;

namespace Demo.EnumVsEnumeration.Benchmark
{
    public class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<Benchmark>();
        }
    }
}