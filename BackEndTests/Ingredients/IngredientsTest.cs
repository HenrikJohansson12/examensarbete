

using System.Text.Json;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using API;
namespace BackEndTests.Ingredients;
using Database.Models.Livsmedelsverket;
public class IngredientsTest
{
  //  [Fact]
    
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
                SlvIngredientId = livsmedel.nummer,
                Name = livsmedel.namn,
                Version = livsmedel.version,
                Type = livsmedel.livsmedelsTyp,
                Number = livsmedel.livsmedelsTypId
            });
        }
        
       Assert.Equal(10,listOfIngredients.Count);
    }

   // [Fact] 
    //Use this test to seed the ingredients to the DB. 
    public async void Can_Save_Ingredient_To_Db()
    {
        var factory = new DesignTimeDbContextFactory();
        var dbContext = factory.CreateDbContext(new string[] { });

        var httpClient = new HttpClient();

        var apiResponse = await 
            httpClient.GetAsync(
                " https://dataportal.livsmedelsverket.se/livsmedel/api/v1/livsmedel?offset=0&limit=3000&sprak=1");

        string jsonData = await apiResponse.Content.ReadAsStringAsync();
        
        Root? root =
            JsonSerializer.Deserialize<Root>(jsonData);

        var listOfIngredients = new List<Ingredient>();

        foreach (var livsmedel in root.livsmedel)
        {
            listOfIngredients.Add(new Ingredient()
            {
                SlvIngredientId = livsmedel.nummer,
                Name = livsmedel.namn,
                Version = livsmedel.version,
                Type = livsmedel.livsmedelsTyp,
                Number = livsmedel.livsmedelsTypId
            });
        }

        await dbContext.Ingredients.AddRangeAsync(listOfIngredients);
      await  dbContext.SaveChangesAsync();
  
    }
    
    [Fact]
    //Use this test to seed the ingredients to the DB. 
    public async void AddIngredientsToDatabase()
    {
        var factory = new DesignTimeDbContextFactory();
        var dbContext = factory.CreateDbContext(new string[] { });
        
        var listOfIngredients = new List<Ingredient>
        {
            new() { Name = "Salt", SlvIngredientId = 0 },
            new() { Name = "Svart peppar", SlvIngredientId = 0 },
            new() { Name = "Vitpeppar", SlvIngredientId = 0 },
            new Ingredient() { Name = "Vitlökspulver", SlvIngredientId = 0 },
            new Ingredient() { Name = "Paprikapulver", SlvIngredientId = 0 },
            new Ingredient() { Name = "Grillkrydda", SlvIngredientId = 0 },
            new Ingredient() { Name = "Oregano", SlvIngredientId = 0 },
            new Ingredient() { Name = "Timjan", SlvIngredientId = 0 },
            new Ingredient() { Name = "Basilika", SlvIngredientId = 0 },
            new Ingredient() { Name = "Rosmarin", SlvIngredientId = 0 },
            new Ingredient() { Name = "Kummin", SlvIngredientId = 0 },
            new Ingredient() { Name = "Koriander", SlvIngredientId = 0 },
            new Ingredient() { Name = "Chilipulver", SlvIngredientId = 0 },
            new Ingredient() { Name = "Kanel", SlvIngredientId = 0 },
            new Ingredient() { Name = "Ingefära", SlvIngredientId = 0 },
            new Ingredient() { Name = "Lökpulver", SlvIngredientId = 0 },
            new Ingredient() { Name = "Cayennepeppar", SlvIngredientId = 0 },
            new Ingredient() { Name = "Dill", SlvIngredientId = 0 },
            new Ingredient() { Name = "Persilja", SlvIngredientId = 0 },
            new Ingredient() { Name = "Lagerblad", SlvIngredientId = 0 },
            new Ingredient() { Name = "Muskotnöt", SlvIngredientId = 0 },
            new Ingredient() { Name = "Spiskummin", SlvIngredientId = 0 },
            new Ingredient() { Name = "Sellerisalt", SlvIngredientId = 0 },
            new Ingredient() { Name = "Senapspulver", SlvIngredientId = 0 },
            new Ingredient() { Name = "Saffran", SlvIngredientId = 0 },
            new Ingredient() { Name = "Okänd",  SlvIngredientId = 0 },
        };
        
        await dbContext.Ingredients.AddRangeAsync(listOfIngredients);
        await  dbContext.SaveChangesAsync();
  
    }
}