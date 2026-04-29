using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;

namespace KHHub.LanguageService.Controllers;

[Route("api/language-management/demo")]
[Area("language-management")]
[RemoteService(Name = "LanguageService")]
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