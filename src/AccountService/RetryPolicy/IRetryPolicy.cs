using AccountService.EventMetadata;

namespace AccountService.RetryPolicy;

public interface IRetryPolicy
{
    public Retry GetNextRetry(Retry? retry);
}