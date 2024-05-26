using API.Requests;
using API.Responses;
using API.Services;
namespace API.Endpoints;

public class SearchIngredientsEndpoint:Endpoint<SearchIngredientRequest, SearchIngredientResponse>
{
    private IIngredientService _ingredientService;

    public SearchIngredientsEndpoint(IIngredientService ingredientService)
    {
        _ingredientService = ingredientService;
    }
    
    public override void Configure()
    {
        Get("/api/searchIngredients");
    }

    public override async Task HandleAsync(SearchIngredientRequest req, CancellationToken ct)
    {
        if (String.IsNullOrWhiteSpace(req.SearchWord))
        {
            await  SendErrorsAsync(400,cancellation: ct);
        }
        else
        {
            Response.Ingredients = await _ingredientService.SearchIngredient(req.SearchWord);
            await SendAsync(Response, cancellation: ct);
        }
    }
}
