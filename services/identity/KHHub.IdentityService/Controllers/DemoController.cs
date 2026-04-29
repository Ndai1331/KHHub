using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace KHHub.IdentityService.Controllers;

[Route("api/identity/demo")]
[Area("identity")]
[RemoteService(Name = "IdentityService")]
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