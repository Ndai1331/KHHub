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

namespace KHHub.MasterDataService.Data.JobFavorites;

public class EfCoreJobFavoriteRepository : EfCoreJobFavoriteRepositoryBase, IJobFavoriteRepository
{
    public EfCoreJobFavoriteRepository(IDbContextProvider<MasterDataServiceDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}