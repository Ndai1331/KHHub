using System;
using System.Linq;
using System.Threading.Tasks;
using KHHub.MasterDataService.Services.Dtos.Provinces;
using KHHub.MasterDataService.Services.Provinces;

namespace KHHub.Web.Address;

/// <summary>
/// Fixed default province (administrative code) for Jobs/Places address UI.
/// </summary>
public static class KhHubDefaultProvinceAddress
{
    public const string DefaultProvinceCode = "56";

    public static async Task<(Guid Id, string Name)?> TryGetAsync(IProvincesAppService provincesAppService)
    {
        var result = await provincesAppService.GetListAsync(
            new GetProvincesInput { Code = DefaultProvinceCode, MaxResultCount = 1 }
        );
        var p = result.Items.FirstOrDefault();
        if (p == null)
        {
            return null;
        }

        return (p.Id, p.Name);
    }
}
