using System.Diagnostics.Metrics;
using Volo.Abp.DependencyInjection;

namespace KHHub.GdprService;

public class KHHubMetrics : ISingletonDependency
{
    public const string MeterName = "KHHub.Api";

    private readonly Counter<long> _helloRequestCounter;

    public KHHubMetrics(IMeterFactory meterFactory)
    {
        var meter = meterFactory.Create(MeterName);
        _helloRequestCounter = meter.CreateCounter<long>("hello_requests.count");
    }

    public void IncrementHelloCounter()
    {
        _helloRequestCounter.Add(1);
    }
}