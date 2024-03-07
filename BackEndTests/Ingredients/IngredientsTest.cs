

using System.Text.Json;
using Database.Models;

namespace BackEndTests.Ingredients;
using Database.Models.Livsmedelsverket;
public class IngredientsTest
{
    [Fact]
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
            //Lägg till här konverteringen. 
        }
    }
}