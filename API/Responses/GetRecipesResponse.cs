using API.Mappers;
using API.Models.DTOS;
using Database.Models;

namespace API.Responses;

public class GetRecipesResponse
{
    public List<RecipeDTO> Recipes { get; set; }
}

