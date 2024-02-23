using API.Properties.Services;
using API.Requests;
using FastEndpoints;

namespace API.Endpoints;

public class GetDiscountedItemsWillysEndpoint:Endpoint<GetDiscountedItemsWillysRequest>
{
    private  IWillysService _willysService;

    public GetDiscountedItemsWillysEndpoint(IWillysService willysService)
    {
        _willysService = willysService;
    }

    public override void Configure()
    {
        Get("/api/getdiscounteditems/willys");
        AllowAnonymous();
    }

    public override async Task HandleAsync(GetDiscountedItemsWillysRequest req, CancellationToken ct)
    {
        
     if (await _willysService.GetDiscountedProducts(req))
     {
         await SendNoContentAsync(ct);
     }
     else
     {
         await SendAsync(500, cancellation: ct);
     }
     
    }
}