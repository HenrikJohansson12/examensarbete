using API.Requests;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using IngredientToRecipe = Database.Models.Livsmedelsverket.IngredientToRecipe;

namespace API.Services;

public interface IRecipeService
{
    Task<bool> SaveRecipe(SaveRecipeRequest req);
    Task<List<Recipe>> GetAllRecipes();
}

public class RecipeService : IRecipeService
{
    private WebApiDbContext _dbContext;

    public RecipeService(WebApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<bool> SaveRecipe(SaveRecipeRequest req)
    {
        try
        {
            var ingredientToRecipeList = new List<IngredientToRecipe>();

            foreach (var ingredient in req.Ingredients)
            {
                var dbIngredient = await _dbContext.Ingredients.FirstOrDefaultAsync(x => x.SlvIngredientId == ingredient.Ingredient.IngredientId);
                if (dbIngredient == null)
                {
                    // Handle case where ingredient does not exist in the database
                    throw new Exception($"Ingredient with ID {ingredient.Ingredient.IngredientId} not found.");
                }

                var ingredientToRecipe = new IngredientToRecipe()
                {
                    Ingredient = dbIngredient,
                    IngredientId = dbIngredient.Id,
                    Amount = ingredient.Amount,
                    Unit = ingredient.Unit
                };
                ingredientToRecipeList.Add(ingredientToRecipe);
            }

            var recipe = new Recipe()
            {
                Name = req.Name,
                Difficulty = req.Difficulty,
                NrOfPortions = req.NrOfPortions,
                Minutes = req.Minutes,
                Instructions = req.Instructions,
                Ingredients = ingredientToRecipeList
            };

            _dbContext.Recipes.Add(recipe);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<List<Recipe>> GetAllRecipes()
    {
        return await _dbContext.Recipes.ToListAsync();
    }
}