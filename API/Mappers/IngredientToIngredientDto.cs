using API.Models.DTOS;
using Database.Models;

namespace API.Mappers;

public class IngredientToIngredientDto
{
    public static IngredientDTO To(Ingredient ingredient)
    {
        return new IngredientDTO()
        {
            Id = ingredient.Id,
            Name = ingredient.Name,
            SlvIngredientId = ingredient.SlvIngredientId
        };
    }
}