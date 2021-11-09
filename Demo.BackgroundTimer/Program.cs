// See https://aka.ms/new-console-template for more information

using Demo.BackgroundTimer;
using System;
using System.Threading.Tasks;

class Program
{
  public static async Task Main(string[] args)
  {
    Console.WriteLine("Hello, World!");

    var task = new BackgroundTask();
    task.Start(TimeSpan.FromSeconds(2), () => Task.Run(() => Console.WriteLine($"Tick, {DateTime.UtcNow:O}")));

    Console.ReadKey();

    await task.StopAsync();

    Console.ReadKey();
  }
}
