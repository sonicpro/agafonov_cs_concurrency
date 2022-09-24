using System;
using System.Linq;
using System.Threading;

namespace RaceCondition
{
    class Program
    {
        const int iterations = 10000;

        static void Main()
        {
            var result = ProcessData();
            Console.WriteLine(result);
        }

        private static int ProcessData()
        {
            var counter = 0;
            ThreadStart proc = () =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    counter++;
                    Thread.SpinWait(10);
                    counter--;
                }
            };
            var tasks = Enumerable.Range(1, 4)
                .Select(_ => new Thread(proc))
                .ToArray();
            foreach (Thread thread in tasks)
            {
                thread.Start();
            }
            
            foreach (Thread thread in tasks)
            {
                thread.Join();
            }

            return counter;
        }
    }
}

