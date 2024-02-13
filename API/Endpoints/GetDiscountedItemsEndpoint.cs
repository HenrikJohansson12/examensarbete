using API.Properties.Services;
using FastEndpoints;

namespace API.Endpoints;

public class GetDiscountedItemsEndpoint:EndpointWithoutRequest
{
    private IIcaService _icaService;

    public GetDiscountedItemsEndpoint(IIcaService icaService)
    {
        _icaService = icaService;
    }

    public override void Configure()
    {
        Get("/api/hello");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        _icaService.GetDiscountedProducts();
        await SendOkAsync();
    }
}