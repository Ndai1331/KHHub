using KHHub.LanguageService.Tests;
using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("KHHub.LanguageService.csproj"); 
await builder.RunAbpModuleAsync<LanguageServiceTestsModule>(applicationName: "KHHub.LanguageService");

public partial class TestProgram
{
}