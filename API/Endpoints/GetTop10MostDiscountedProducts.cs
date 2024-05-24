using API.Mappers;
using API.Responses;
using API.Services;
using Database;
using Microsoft.EntityFrameworkCore;

namespace API.Endpoints;

public class GetTop10MostDiscountedProducts: EndpointWithoutRequest<GetTop10MostDiscountedProductsResponse>
{
    private IProductService _productService;

    public GetTop10MostDiscountedProducts(IProductService productService)
    {
        _productService = productService;
    }
    public override void Configure()
    {
        Get("/api/toptendiscountedproducts");
    }

    public override async Task HandleAsync( CancellationToken ct)
    {
       Response.Products = await  _productService.GetTopTenOffers();

    
        await SendAsync(Response);

    }
}