using AccountService.EventMetadata;

namespace AccountService.RetryPolicy;

public class ConstantRetryPolicy(int retryDelayInMs) : IRetryPolicy
{
    public Retry GetNextRetry(Retry? retry)
    {
        return new Retry
        {
            LastAttempt = DateTime.UtcNow,
            NextAttempt = DateTime.UtcNow.AddMilliseconds(retryDelayInMs),
            AttemptCount = (retry?.AttemptCount ?? 0) + 1,
            LastDelayInMs = retryDelayInMs
        };
    }
}