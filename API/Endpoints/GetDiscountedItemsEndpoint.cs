using API.Properties.Services;
using FastEndpoints;

namespace API.Endpoints;

public class GetDiscountedItemsEndpoint:EndpointWithoutRequest
{
    private readonly IStoreServiceFactory _storeServiceFactory;

    public GetDiscountedItemsEndpoint(IStoreServiceFactory storeServiceFactory)
    {
        _storeServiceFactory = storeServiceFactory;
    }

    public override void Configure()
    {
        Get("/api/hello");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var storeService = _storeServiceFactory.Create("Willys");
        storeService.GetDiscountedProducts();
        await SendOkAsync();
    }
}