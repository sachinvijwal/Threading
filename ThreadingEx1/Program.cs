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
            public int Number { get; internal set; }
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
                    results.Enqueue(Square(j++));
                }));
            }

            Task.WaitAll(tasks.ToArray());

            foreach (var result in results)
            {
                Console.WriteLine($"Square of { result.Number} is { result.Square }.");
            }

            Console.ReadKey();
        }

        private static Result Square(int num)
        {
            int sleep = new Random().Next(1000, 10000);
            Console.WriteLine(sleep);
            Thread.Sleep(sleep);
            return new Result
            {
                Number = num,
                Square = num * num
            };
        }
    }
}
