using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using KHHub.MasterDataService.Data;

namespace KHHub.MasterDataService.Data.PlaceFavorites;

public class EfCorePlaceFavoriteRepository : EfCorePlaceFavoriteRepositoryBase, IPlaceFavoriteRepository
{
    public EfCorePlaceFavoriteRepository(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}