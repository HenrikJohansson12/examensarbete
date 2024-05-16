using API.Mappers;
using API.Models.DTOS;
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
      var recipes=  await _recipeService.GetAllRecipes();
      var recipeDto = new List<RecipeDTO>();
      foreach (var recipe in recipes)
      {
          recipeDto.Add(RecipeToRecipeDto.To(recipe));
      }

      Response.Recipes = recipeDto;
        await SendAsync(Response, cancellation: ct);
    }
}