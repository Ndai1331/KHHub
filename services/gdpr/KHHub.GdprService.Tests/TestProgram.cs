using KHHub.GdprService.Tests;
using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("KHHub.GdprService.csproj"); 
await builder.RunAbpModuleAsync<GdprServiceTestsModule>(applicationName: "KHHub.GdprService");

public partial class TestProgram
{
}
