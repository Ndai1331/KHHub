using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace KHHub.AdministrationService.Controllers;

[Route("api/administration/demo")]
[Area("administration")]
[RemoteService(Name = "AdministrationService")]
public class DemoController : AbpController
{
    private readonly KHHubMetrics _kHHubMetrics;

    public DemoController(KHHubMetrics kHHubMetrics)
    {
        _kHHubMetrics = kHHubMetrics;
    }
    
    [HttpGet]
    [Route("hello")]
    public async Task<string> HelloWorld()
    {
        _kHHubMetrics.IncrementHelloCounter();
        return await Task.FromResult("Hello World!");
    }
}