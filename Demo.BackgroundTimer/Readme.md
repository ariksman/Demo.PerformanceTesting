## Background task runner class with PeriodicTimer (.NET 6)

.NET 6 introduces a new timer type called PeriodicTimer, it is designed to be used in an asynchronous context.
The main purpose behind the PeriodicTimer is to avoid using callbacks. 

``` CSharp
var second = TimeSpan.FromSeconds(1);
using var timer = new AsyncTimer(second);
while (await timer.WaitForNextTickAsync())
{
    Console.WriteLine($"Tick {DateTime.Now}")
}
```

[Documentation for PeriodicTimer](https://docs.microsoft.com/en-us/dotnet/api/system.threading.periodictimer?view=net-6.0)
