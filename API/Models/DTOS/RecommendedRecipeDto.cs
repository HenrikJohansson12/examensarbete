namespace API.Models.DTOS;

public class RecommendedRecipeDto
{
        public int Id { get; set; }
        public string Name { get; set; }
        public int Minutes { get; set; }
        public int NrOfPortions { get; set; }
        public int Difficulty { get; set; }
        public string Instructions { get; set; }
        public List<RecommendedRecipeIngredients> Ingredients { get; set; }
}

public class RecommendedRecipeIngredients
{
  
        public  string  Name { get; set; }
        public int Amount { get; set; }
        public string Unit { get; set; }
        public ProductRecordDto? Offer { get; set; }

}