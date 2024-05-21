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
    var  searchResults =  await    _dbContext.Ingredients.Where(i => i.Name.ToLower().Contains(searchWord)).ToListAsync();
    var response = new List<IngredientDTO>();
    for (int i = 0; i < searchResults.Count; i++)
    {
        response.Add(IngredientToIngredientDto.To(searchResults[i]));
    }

    return response;
    }
}