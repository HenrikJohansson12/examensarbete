using API.Models.DTOS;
using Database.Models;

namespace API.Mappers;

public class RecipeToRecipeDto
{
    public static RecipeDTO To(Recipe recipe)
    {
        var ingredientDTOList = new List<IngredientToRecipeDto>();
        foreach (var ingredient in recipe.Ingredients)
        {
            var ingredientToRecipeDTO = new IngredientToRecipeDto()
            {
                Id = ingredient.Id,
                Unit = ingredient.Unit,
                Amount = ingredient.Amount,
                Ingredient = new IngredientDTO()
                {
                    Id = ingredient.Ingredient.Id,
                    Name = ingredient.Ingredient.Name,
                    SlvIngredientId = ingredient.Ingredient.SlvIngredientId
                }
            };
            ingredientDTOList.Add(ingredientToRecipeDTO);
        }
        var dto = new RecipeDTO()
        {
           Id = recipe.Id,
           Name = recipe.Name,
           Minutes = recipe.Minutes,
           NrOfPortions = recipe.NrOfPortions,
           Difficulty = recipe.Difficulty,
           Instructions = recipe.Instructions,
           Ingredients = ingredientDTOList
        };
        return dto;
    }
}