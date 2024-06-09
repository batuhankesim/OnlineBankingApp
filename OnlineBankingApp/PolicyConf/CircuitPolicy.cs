using Polly.CircuitBreaker;
using StackExchange.Redis;
using Polly;

namespace OnlineBankingApp.PolicyConf
{
    public static class CircuitPolicy
    {
        public static AsyncCircuitBreakerPolicy CreatePolicy(
             int exceptionsAllowedBeforeBreaking,
             TimeSpan durationOfBreak,
             ILogger logger)
        {
            return Policy
                .Handle<RedisConnectionException>()
                .Or<Exception>()
                .CircuitBreakerAsync(
                    exceptionsAllowedBeforeBreaking,
                    durationOfBreak,
                    onBreak: (exception, breakDelay) =>
                    {
                        logger.LogError($"Circuit breaker opened for {breakDelay.TotalSeconds} seconds due to: {exception.Message}");
                    },
                    onReset: () =>
                    {
                        logger.LogInformation("Circuit breaker reset.");
                    },
                    onHalfOpen: () =>
                    {
                        logger.LogInformation("Circuit breaker is half-open. Next call is a trial.");
                    }
                );
        }
    }
    
}
