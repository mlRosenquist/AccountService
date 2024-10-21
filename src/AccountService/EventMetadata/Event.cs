using AccountService.Models;

namespace AccountService.EventMetadata;

public abstract record Event<T> where T : Entity
{
    public DateTime Timestamp { get; init; }
    public T Payload { get; init; }

    public Retry? Retry { get; set; }
    
    public override string ToString()
    {
        return $"Timestamp: {Timestamp}, Payload: {Payload} Retry: {Retry}";
    }
}