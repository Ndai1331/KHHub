using System;

namespace KHHub.Web.Pages.Shared;

public class TagPickerViewModel
{
    /// <summary>article | place</summary>
    public string Kind { get; init; } = "";

    public Guid? EntityId { get; init; }
}
