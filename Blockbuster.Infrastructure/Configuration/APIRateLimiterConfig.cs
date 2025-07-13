namespace Blockbuster.Infrastructure.Configuration;


public class APIRateLimiterConfig
{
    public int AllowedRequestsPerMinute { get; set; } = 100;

    public int QueueLimit { get; set; } = 10;
}
