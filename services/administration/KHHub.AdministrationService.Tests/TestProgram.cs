using KHHub.AdministrationService.Tests;
using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("KHHub.AdministrationService.csproj"); 
await builder.RunAbpModuleAsync<AdministrationServiceTestsModule>(applicationName: "KHHub.AdministrationService");

public partial class TestProgram
{
}
