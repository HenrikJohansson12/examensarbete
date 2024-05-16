using Database.Models;

namespace API.Responses;

public class GetRecipesResponse
{
    public List<Recipe> Recipes { get; set; }
}