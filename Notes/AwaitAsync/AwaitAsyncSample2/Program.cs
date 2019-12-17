using System;
using System.Threading;
using System.Threading.Tasks;

namespace AwaitAsyncSample2
{
    class Program
    {
        static void Main(string[] args)
        {
              Show().Wait();

        }

        public static async Task Show()
        {
            Console.WriteLine($"Show Start {Thread.CurrentThread.ManagedThreadId.ToString("00")}");

            await Async();

            Console.WriteLine($"Show End {Thread.CurrentThread.ManagedThreadId.ToString("00")}");
        }

        public static async Task Async()
        {
            Console.WriteLine($"Async Start {Thread.CurrentThread.ManagedThreadId.ToString("00")}");

            await Task.Run(() =>
            {
                Thread.Sleep(500);
                Console.WriteLine($"Async TaskRun {Thread.CurrentThread.ManagedThreadId.ToString("00")}");
            });

            Console.WriteLine($"Async End {Thread.CurrentThread.ManagedThreadId.ToString("00")}");
        }
    }
}
