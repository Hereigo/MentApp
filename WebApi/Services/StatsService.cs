using WebApi.ApiModels;
using Microsoft.Extensions.Options;

namespace WebApi.Services;

public class StatsService_OLD
{
    private int _requestCounter;
    private int _itemsMax;
    private readonly IConfiguration Configuration;

    public StatsService_OLD(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void IncrementStatsCounter()
    {
        var configItems = new ConfigItems(10); // default value

        Configuration.GetSection(ConfigItems.SectionItems).Bind(configItems);

        _itemsMax = configItems.Max;

        // Thread safety atomic operation, without interruption from other threads, to avoid race conditions.
        Interlocked.Increment(ref _requestCounter);
    }

    public int GetStatsCounter() => _requestCounter;
}
// -----------------------------------------------------------

// THE SAME AS ABOVE, BUT USING: IOptions<T>

public class StatsService
{
    private int _requestCounter;
    private int _itemsMax;
    private readonly ConfigItems _configItems;

    public StatsService(IOptions<ConfigItems> options)
    {
        _configItems = options.Value;
    }

    public void IncrementStatsCounter()
    {
        _itemsMax = _configItems.Max;

        // Thread safety atomic operation, without interruption from other threads, to avoid race conditions.
        Interlocked.Increment(ref _requestCounter);
    }

    public int GetStatsCounter() => _requestCounter;
}