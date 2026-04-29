using KHHub.AuditLoggingService.Tests;
using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("KHHub.AuditLoggingService.csproj"); 
await builder.RunAbpModuleAsync<AuditLoggingServiceTestsModule>(applicationName: "KHHub.AuditLoggingService");

public partial class TestProgram
{
}
