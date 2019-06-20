using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadingEx1
{
    class Program
    {
        static void Main(string[] args)
        {
            Example1();
        }

        public class Result
        {
            public int Square { get; set; }
            public int Number { get; set; }
            public Thread CurrentThread { get; set; }
            public int ThreadSleep { get; set; }
        }

        private static void Example1()
        {
            var results = new System.Collections.Concurrent.ConcurrentQueue<Result>();

            List<Task> tasks = new List<Task>();
            int j = 1;
            for (int i = 1; i <= 10; i++)
            {
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    results.Enqueue(Square(j++, Thread.CurrentThread));
                }));
            }

            Task.WaitAll(tasks.ToArray());

            foreach (var result in results.OrderBy(x => x.CurrentThread.ManagedThreadId))
            {
                Console.WriteLine($"{ result.CurrentThread.ManagedThreadId }. Square of { result.Number} is { result.Square }. " +
                    $"\t\t\t Thread wait time was - { result.ThreadSleep }");
            }

            Console.ReadKey();
        }

        private static Result Square(int num, Thread currentThread)
        {
            int sleep = new Random().Next(100, 5000) / 2;
            Thread.Sleep(sleep);
            return new Result
            {
                Number = num,
                Square = num * num,
                CurrentThread = currentThread,
                ThreadSleep = sleep
            };
        }
    }
}
