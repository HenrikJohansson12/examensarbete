using API.Properties.Services;
using API.Properties.Services;
using API.Requests;
using FastEndpoints;
namespace API.Endpoints;

public class GetDiscountedItemsIcaEndpoint:Endpoint<GetDiscountedItemsIcaRequest>
{
    private  IIcaService _icaService;

    public GetDiscountedItemsIcaEndpoint(IIcaService icaService)
    {
        _icaService = icaService;
    }

    public override void Configure()
    {
        Get("/api/getdiscounteditems/ica");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetDiscountedItemsIcaRequest req, CancellationToken ct)
    {
        
        if (await _icaService.GetDiscountedProducts(req))
        {
            await SendNoContentAsync(ct);
        }
        else
        {
            await SendAsync(500, cancellation: ct);
        }
     
    }
}