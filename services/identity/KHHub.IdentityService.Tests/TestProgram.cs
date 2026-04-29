using KHHub.IdentityService.Tests;
using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("KHHub.IdentityService.csproj"); 
await builder.RunAbpModuleAsync<IdentityServiceTestsModule>(applicationName: "KHHub.IdentityService");

public partial class TestProgram
{
}
