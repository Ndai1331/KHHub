using KHHub.MasterDataService.Entities.Jobs;
using System;
using System.Collections.Generic;
using KHHub.MasterDataService.Entities.JobFavorites;

namespace KHHub.MasterDataService.Entities.JobFavorites;

public abstract class JobFavoriteWithNavigationPropertiesBase
{
    public JobFavorite JobFavorite { get; set; } = null!;
    public Job Job { get; set; } = null!;
}