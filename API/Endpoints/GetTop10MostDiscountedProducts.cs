using API.Responses;
using Database;
using Microsoft.EntityFrameworkCore;

namespace API.Endpoints;

public class GetTop10MostDiscountedProducts: EndpointWithoutRequest<GetTop10MostDiscountedProductsResponse>
{
    private WebApiDbContext _dbContext;

    public GetTop10MostDiscountedProducts(WebApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public override void Configure()
    {
        Get("/api/test");
        AllowAnonymous();
    }

    public override async Task HandleAsync( CancellationToken ct)
    {
        Response.Result = await _dbContext.ProductRecords.ToListAsync();
        await SendAsync(Response);

    }
}