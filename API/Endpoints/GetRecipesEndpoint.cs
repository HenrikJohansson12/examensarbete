using API.Responses;
using API.Services;

namespace API.Endpoints;

public class GetRecipesEndpoint : EndpointWithoutRequest<GetRecipesResponse>
{
    private IRecipeService _recipeService;

    public GetRecipesEndpoint(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    public override void Configure()
    {
        Get("/api/getrecipes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Response.Recipes = await _recipeService.GetAllRecipes();
        await SendAsync(Response, cancellation: ct);
    }
}