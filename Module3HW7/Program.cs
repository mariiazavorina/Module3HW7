using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace Module3HW7
{
    public class Program
    {
        public static int Fibonacci(int n, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return -1;
            }

            if (n == 0)
            {
                return 0;
            }
            else if (n == 1)
            {
                return 1;
            }
            else
            {
                return Fibonacci(n - 1, token) + Fibonacci(n - 2, token);
            }
        }

        public static void FibonacciNumber()
        {
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            for (var i = 0; ; i++)
            {
                var task = Task.Run(() => Fibonacci(i, token));

                if (task.Wait(TimeSpan.FromSeconds(10)))
                {
                    Console.WriteLine(task.Result);
                }
                else
                {
                    source.Cancel();
                    Console.WriteLine("Cancel");
                    break;
                }
            }
        }

        private static async Task Main(string[] args)
        {
            Thread myThread = new Thread(new ThreadStart(FibonacciNumber));
            var taskList = new List<Task>();
            var tasks = new Tasks();
            taskList.Add(Task.Run(() => tasks.Add(1, 2)));
            taskList.Add(Task.Run(() => tasks.Sub(5, 3)));
            taskList.Add(Task.Run(() => tasks.Multiply(2, 2)));
            taskList.Add(Task.Run(() => tasks.Divide(5, 3)));

            await Task.WhenAll(taskList);
            myThread.Start();
            Console.WriteLine();
        }
    }
}