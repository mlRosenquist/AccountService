using AccountService.EventMetadata;
using AccountService.RetryPolicy;

namespace AccountServiceTest.RetryPolicy;

public class ConstantRetryPolicyTest
{
    private readonly ConstantRetryPolicy _uut = new(1000);
    
    [Fact]
    public void GetNextRetry_WhenRetryIsNull_ReturnsDefaultValues()
    {
        Retry? retry = null;
        var result = _uut.GetNextRetry(retry);
        
        Assert.Equal(1, result.AttemptCount);
        Assert.Equal(1000, result.LastDelayInMs);
        Assert.Equal(DateTime.UtcNow, result.LastAttempt, TimeSpan.FromSeconds(1));
        Assert.Equal(DateTime.UtcNow.AddMilliseconds(1000), result.NextAttempt, TimeSpan.FromSeconds(1));
    }
    
    [Fact]
    public void GetNextRetry_MultipleRequests_ReturnsAdditiveValues()
    {
        var first = _uut.GetNextRetry(null);
        var result = _uut.GetNextRetry(first);
        
        Assert.Equal(2, result.AttemptCount);
    }
}