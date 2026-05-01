using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;
using KHHub.MasterDataService.Services.Dtos.Shared;
using KHHub.MasterDataService.Entities.Places;
using KHHub.MasterDataService.Entities.PlaceTags;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using KHHub.MasterDataService.Services.PlaceTagMappings;

namespace KHHub.Web.Pages.PlaceTagMappings;

public class EditModalModel : EditModalModelBase
{
    public EditModalModel(IPlaceTagMappingsAppService placeTagMappingsAppService) : base(placeTagMappingsAppService)
    {
    }
}