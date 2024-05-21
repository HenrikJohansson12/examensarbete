using API.Models.DTOS;

namespace API.Responses;

public class SearchIngredientResponse
{
    public List<IngredientDTO> Ingredients { get; set; }
}