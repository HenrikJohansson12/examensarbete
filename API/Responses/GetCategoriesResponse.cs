using Database.Models;

namespace API.Responses;

public class GetCategoriesResponse
{
    public List<Category> Categories { get; set; }
}