using KHHub.AIManagementService.Tests;
using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("KHHub.AIManagementService.csproj"); 
await builder.RunAbpModuleAsync<AIManagementServiceTestsModule>(applicationName: "KHHub.AIManagementService");

public partial class TestProgram
{
}
