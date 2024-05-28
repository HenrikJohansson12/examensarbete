using API.Models.DTOS;

namespace API.Responses;

public class GetRecommendedRecipesResponse
{
    public List<RecommendedRecipeDto> Recipes { get; set; }
}