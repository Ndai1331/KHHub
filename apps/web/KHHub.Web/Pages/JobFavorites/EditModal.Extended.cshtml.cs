using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Jobs;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.JobFavorites;

namespace KHHub.Web.Pages.JobFavorites;

public class EditModalModel : EditModalModelBase
{
    public EditModalModel(IJobFavoritesAppService jobFavoritesAppService) : base(jobFavoritesAppService)
    {
    }
}