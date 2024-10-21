namespace AccountService.Loggers;

public class ConsoleLogger(string prefix) : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine($"[{prefix}] {message}");
    }
}