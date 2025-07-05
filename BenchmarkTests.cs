using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using System.Linq;

namespace _5_Amazing_Libraries
{
   

    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class BenchmarkTests
    {
        private int[] data;

        [GlobalSetup]
        public void Setup()
        {
            data = Enumerable.Range(1, 10_000).ToArray();
        }

        [Benchmark]
        public int ForLoopTest()
        {
            int sum = 0;
            for (int i = 0; i < data.Length; i++)
                sum += data[i];
            return sum;
        }

        [Benchmark]
        public int LinqTest()
        {
            return data.Sum();
        }

        [Benchmark]
        public int ParallelTest()
        {
            return data.AsParallel().Sum();
        }
    }

}
