using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Places;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.PlaceFavorites;

namespace KHHub.Web.Pages.PlaceFavorites;

public class EditModalModel : EditModalModelBase
{
    public EditModalModel(IPlaceFavoritesAppService placeFavoritesAppService) : base(placeFavoritesAppService)
    {
    }
}