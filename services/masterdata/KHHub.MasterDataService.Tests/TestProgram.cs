using KHHub.MasterDataService.Tests;
using Microsoft.AspNetCore.Builder;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("KHHub.MasterDataService.csproj"); 
await builder.RunAbpModuleAsync<MasterDataServiceTestsModule>(applicationName: "KHHub.MasterDataService");

public partial class TestProgram
{
}
