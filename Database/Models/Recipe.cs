namespace Database.Models;

public class Recipe
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal TimeInHours { get; set; }
    public int NrOfPortions { get; set; }
    public int Difficulty { get; set; }
    public string Instructions { get; set; }
    public string ImageLink { get; set; }
    public IEnumerable<string> Ingredients { get; set; }
}