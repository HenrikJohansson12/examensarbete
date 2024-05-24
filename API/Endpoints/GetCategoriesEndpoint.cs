using API.Responses;
using API.Services;
using Database;

namespace API.Endpoints;

public class GetCategoriesEndpoint : EndpointWithoutRequest<GetCategoriesResponse>
{
    private ICategoryService _categoryService;

    public GetCategoriesEndpoint(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }
    
    public override void Configure()
    {
        Get("/api/getcategories");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Response.Categories =  await _categoryService.GetCategories();

        await SendAsync(Response, cancellation: ct);
    }
}