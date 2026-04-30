using KHHub.MasterDataService.Entities.Provinces;
using System;
using System.Collections.Generic;
using KHHub.MasterDataService.Entities.Wards;

namespace KHHub.MasterDataService.Entities.Wards;

public abstract class WardWithNavigationPropertiesBase
{
    public Ward Ward { get; set; } = null!;
    public Province Province { get; set; } = null!;
}