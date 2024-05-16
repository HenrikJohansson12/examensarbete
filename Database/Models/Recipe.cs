using Database.Models.Livsmedelsverket;

namespace Database.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Minutes { get; set; }
    public int NrOfPortions { get; set; }
    public int Difficulty { get; set; }
    public string Instructions { get; set; }
    
//    public string ImageLink { get; set; }
    public List<IngredientToRecipe> Ingredients { get; set; } = new List<IngredientToRecipe>();
}