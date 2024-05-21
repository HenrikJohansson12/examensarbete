using API.Mappers;
using API.Models.DTOS;
using Database;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface IIngredientService
{
    Task<List<IngredientDTO>> SearchIngredient(string searchWord);
}

internal class IngredientService: IIngredientService
{
    private WebApiDbContext _dbContext;

    public IngredientService( WebApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<IngredientDTO>> SearchIngredient(string searchWord)
    {
        searchWord = searchWord.ToLower();
        var startsWithResults = await _dbContext.Ingredients
            .Where(i => i.Name.ToLower().StartsWith(searchWord))
            .ToListAsync();

        var containsResults = await _dbContext.Ingredients
            .Where(i => i.Name.ToLower().Contains(searchWord) && !i.Name.ToLower().StartsWith(searchWord))
            .ToListAsync();

        var searchResults = startsWithResults.Concat(containsResults).Distinct().ToList();

        var response = searchResults.Select(IngredientToIngredientDto.To).ToList();

        return response;
    }


}