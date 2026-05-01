using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KHHub.MasterDataService.Services.PlaceFavorites;

namespace KHHub.Web.Pages.PlaceFavorites;

public class CreateModalModel : CreateModalModelBase
{
    public CreateModalModel(IPlaceFavoritesAppService placeFavoritesAppService) : base(placeFavoritesAppService)
    {
    }
}