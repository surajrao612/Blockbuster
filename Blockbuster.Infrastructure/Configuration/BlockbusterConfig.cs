namespace Blockbuster.Infrastructure.Configuration;

public class BlockbusterConfig
{
    public string ApiAccessToken {  get; set; } = string.Empty;
    public string CinemaWorldServiceUrl { get; set; } = string.Empty;
    public string FilmWorldServiceUrl { get; set; } = string.Empty;
    public int DataCacheInMinutes { get; set; } = 15;

    public RetryPolicySettings RetryPolicySettings { get; set; } = new();
}

public class RetryPolicySettings
{
    public int RetryCount { get; set; } = 3;
    public int DelaySeconds { get; set; } = 1;
}
