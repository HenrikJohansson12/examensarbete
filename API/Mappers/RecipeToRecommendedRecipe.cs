using API.Models.DTOS;
using Database.Models;

namespace API.Mappers;

public class RecipeToRecommendedRecipe
{
    public static RecommendedRecipeDto To(Recipe recipe)
    {
        var dto = new RecommendedRecipeDto()
        {
            Id = recipe.Id,
            Name = recipe.Name,
            Minutes = recipe.Minutes,
            NrOfPortions = recipe.NrOfPortions,
            Difficulty = recipe.Difficulty,
            Instructions = recipe.Instructions,

        };
        return dto;
    }
}