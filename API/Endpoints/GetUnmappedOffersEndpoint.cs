using API.Responses;
using API.Services;

namespace API.Endpoints;

public class GetUnmappedOffersEndpoint: EndpointWithoutRequest<GetUnmappedOffersResponse>
{
    private IProductService _productService;

    public GetUnmappedOffersEndpoint(IProductService productService)
    {
        _productService = productService;
    }
    
    public override void Configure()
    {
        Get("/api/getunmappedoffers");
         AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Response.Offers  = await _productService.GetUnmappedOffers();
        await SendAsync(Response, cancellation: ct);
    }
}