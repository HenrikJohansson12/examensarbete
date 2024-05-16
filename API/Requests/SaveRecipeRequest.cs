namespace API.Requests;

public class SaveRecipeRequest
{
    public string Name { get; set; }
    public int Minutes { get; set; }
    public int NrOfPortions { get; set; }
    public int Difficulty { get; set; }
    public string Instructions { get; set; }
    public List<IngredientToRecipe> Ingredients { get; set; }
}

public class IngredientToRecipe
    {
        public Ingredient Ingredient { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
    }

    public class Ingredient
    {
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public string Type { get; set; }
        public int Number { get; set; }
        public DateTime Version { get; set; }
        public string Name { get; set; }
    }

