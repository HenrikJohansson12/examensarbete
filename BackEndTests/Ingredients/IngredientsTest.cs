

using System.Text.Json;
using Database.Models;

namespace BackEndTests.Ingredients;
using Database.Models.Livsmedelsverket;
public class IngredientsTest
{
    [Fact]
    
    //Testing that we can call the api and convert the response into a ingredient. 
    public async void Can_CreateIngredient_rows_FromLivsmedelsverket_Api()
    {
        var httpClient = new HttpClient();

        var apiResponse = await 
            httpClient.GetAsync(
                " https://dataportal.livsmedelsverket.se/livsmedel/api/v1/livsmedel?offset=0&limit=10&sprak=1");

        string jsonData = await apiResponse.Content.ReadAsStringAsync();
        
        Root? root =
            JsonSerializer.Deserialize<Root>(jsonData);

        var listOfIngredients = new List<Ingredient>();

        foreach (var livsmedel in root.livsmedel)
        {
            listOfIngredients.Add(new Ingredient()
            {
                IngredientId = livsmedel.nummer,
                Name = livsmedel.namn,
                Version = livsmedel.version,
                Type = livsmedel.livsmedelsTyp,
                Number = livsmedel.livsmedelsTypId
            });
        }
        
       Assert.Equal(10,listOfIngredients.Count);
    }
}