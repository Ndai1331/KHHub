namespace KHHub.Publish_website.Services.GoldPrices;

public sealed class GoldPriceSnapshot
{
    public IReadOnlyList<GoldPriceItem> Data { get; init; } = [];

    public string Source { get; init; } = "sjc.com.vn";

    public DateTimeOffset? UpdatedAt { get; init; }

    public bool Stale { get; init; }

    public bool IsFallback { get; init; }

    public string? ErrorMessage { get; init; }
}

public sealed class GoldPriceItem
{
    public string Type { get; init; } = string.Empty;

    public decimal BuyPrice { get; init; }

    public decimal SellPrice { get; init; }

    public string Region { get; init; } = string.Empty;

    public string Change { get; init; } = "stable";

    public decimal ChangeAmount { get; init; }
}

