namespace Database.Models;

public class Ingredient
{
        public int Id { get; set; }
        public int IngredientId { get; set; }
        public string Type { get; set; }
        public int Number { get; set; }
        public DateTime Version { get; set; }
        public string Name { get; set; }
}