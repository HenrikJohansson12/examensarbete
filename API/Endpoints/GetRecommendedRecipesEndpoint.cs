using API.Responses;
using API.Services;

namespace API.Endpoints;

public class GetRecommendedRecipesEndpoint: EndpointWithoutRequest<GetRecommendedRecipesResponse>
{
    private IRecipeService _recipeService;

    public GetRecommendedRecipesEndpoint(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }

    public override void Configure()
    {
        Get("/api/getrecommendedrecipes");
        AllowAnonymous();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        Response.Recipes = await _recipeService.GetRecommendedRecipes();

        await SendAsync(Response, cancellation: ct);
    }
}