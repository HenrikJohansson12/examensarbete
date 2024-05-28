using API.Requests;
using API.Services;

namespace API.Endpoints;

public class SaveRecipeEndpoint: Endpoint<SaveRecipeRequest>
{
    private IRecipeService _recipeService;

    public SaveRecipeEndpoint(IRecipeService recipeService)
    {
        _recipeService = recipeService;
    }
    
    public override void Configure()
    {
        Post("/api/saverecipe");
       
    }
    public override async Task HandleAsync(SaveRecipeRequest req, CancellationToken ct)
    {
        if (await _recipeService.SaveRecipe(req))
        {
            await SendNoContentAsync(ct);
        }
        else await SendAsync(500, cancellation: ct);
    }
}