using API.Requests;
using API.Services;

namespace API.Endpoints;

public class UpdateOffersEndpoint: Endpoint<UpdateOfferRequest>
{
    private IProductService _productService;

    public UpdateOffersEndpoint(IProductService productService)
    {
        _productService = productService;
    }
    public override void Configure()
    {
        Post("/api/updateOffers");
    }
    public override async Task HandleAsync(UpdateOfferRequest req, CancellationToken ct)
    {
        if (await _productService.MapOffers(req.Offers))
        {
            await SendNoContentAsync(ct);
        }
        else await SendAsync(500, cancellation: ct);
    }
}