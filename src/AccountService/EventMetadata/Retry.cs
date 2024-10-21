namespace AccountService.EventMetadata;

public class Retry
{
    public int LastDelayInMs { get; set; }
    public DateTime LastAttempt { get; set; }
    public DateTime NextAttempt { get; set; }
    public int AttemptCount { get; set; }
    
    public override string ToString() {
        return $"LastDelayInMs: {LastDelayInMs}, LastAttempt: {LastAttempt}, NextAttempt: {NextAttempt}, AttemptCount: {AttemptCount}";
    }
}