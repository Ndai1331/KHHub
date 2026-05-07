namespace KHHub.Publish_website.Services.GoldPrices;

public interface IGoldPriceClient
{
    Task<GoldPriceSnapshot> GetLatestAsync(CancellationToken cancellationToken = default);
}

