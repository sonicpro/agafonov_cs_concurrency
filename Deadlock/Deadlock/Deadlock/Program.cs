using System.Threading;

namespace Deadlock
{
    class Program
    {
        const int iterations = 10000;
        private object _lockOne = new object();
        private object _lockTwo = new object();

        static void Main()
        {
            new Program().StartCompetingThreads();
            System.Console.WriteLine("Done");
        }

        private void StartCompetingThreads()
        {
            ThreadStart taskOne = () =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    lock (_lockOne)
                    {
                        lock (_lockTwo)
                        {
                            Thread.SpinWait(10);
                        }
                    }
                }
            };

            ThreadStart taskTwo = () =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    lock (_lockTwo)
                    {
                        lock (_lockOne)
                        {
                            Thread.SpinWait(10);
                        }
                    }
                }
            };

            var threadOne = new Thread(taskOne);
            var threadTwo = new Thread(taskTwo);
            threadOne.Start();
            threadTwo.Start();
            threadOne.Join();
            threadTwo.Join();
        }
    }
}
