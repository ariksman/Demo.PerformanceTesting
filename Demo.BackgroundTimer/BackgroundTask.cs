using System;

namespace Demo.BackgroundTimer
{
  public class BackgroundTask
  {
    private Task? _timerTask;
    private readonly CancellationTokenSource _cts = new();

    public void StartAsync(TimeSpan timeSpan, Func<Task> action)
    {
      async Task DoStart()
      {
        try
        {
          var timer = new PeriodicTimer(timeSpan);
          while(await timer.WaitForNextTickAsync(_cts.Token))
          {
            await action();
          }
        }
        catch (OperationCanceledException e){}
      }

      _timerTask = DoStart();
    }

    public async Task StopAsync()
    {
      _cts.Cancel();

      await _timerTask;

      _cts.Dispose();

      Console.WriteLine("Background task stopped");
    }
  }
}
