using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public interface ICategoryService
{
    Task<List<Category>> GetCategories();
}
public class CategoryService: ICategoryService
{
    private WebApiDbContext _dbContext;

    public CategoryService(WebApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public Task<List<Category>> GetCategories()
    {
  return   _dbContext.Categories.ToListAsync();
    }
}