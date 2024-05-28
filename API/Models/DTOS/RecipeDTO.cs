namespace API.Models.DTOS;

public class RecipeDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Minutes { get; set; }
    public int NrOfPortions { get; set; }
    public int Difficulty { get; set; }
    public string Instructions { get; set; }
    
//    public string ImageLink { get; set; }
    public List<IngredientToRecipeDto> Ingredients { get; set; } =  new ();
}

public class IngredientToRecipeDto
{
    public int Id { get; set; }
    public IngredientDTO Ingredient { get; set; }
    public int Amount { get; set; }
    public string Unit { get; set; }
    //public string Part { get; set; }
}

public class IngredientDTO
{
    public int Id { get; set; }
    public int? SlvIngredientId { get; set; }
    
    public string Name { get; set; }
}