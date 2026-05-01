using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Wards;
using KHHub.MasterDataService.Entities.Provinces;
using KHHub.MasterDataService.Entities.PlaceCategories;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.Places;

namespace KHHub.Web.Pages.Places;

public class EditModalModel : EditModalModelBase
{
    public EditModalModel(IPlacesAppService placesAppService) : base(placesAppService)
    {
    }
}