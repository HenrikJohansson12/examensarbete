namespace Database.Models.Livsmedelsverket;

public class IngredientToRecipe
{
    public int Id { get; set; }
    public int IngredientId { get; set; }
    public int RecipeId { get; set; }
    public int Amount { get; set; }
    public string Unit { get; set; }
    public string Part { get; set; }
}