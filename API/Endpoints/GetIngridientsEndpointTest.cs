using System.Text.Json;
using API.Mappers;
using API.Responses;
using Database;
using Database.Models;
using Database.Models.DTO;
using Database.Models.Livsmedelsverket;
using Microsoft.EntityFrameworkCore;

namespace API.Endpoints;

public class GetIngredientsTest: EndpointWithoutRequest<TestResponse>
{
    private WebApiDbContext _dbContext;

    public GetIngredientsTest(WebApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public override void Configure()
    {
        Get("/api/test2");
        AllowAnonymous();
    }

    public override async Task HandleAsync( CancellationToken ct)
    {
        var httpClient = new HttpClient();

        var apiResponse = await 
            httpClient.GetAsync(
                " https://dataportal.livsmedelsverket.se/livsmedel/api/v1/livsmedel?offset=0&limit=2500&sprak=1");

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

        Response.Ingredients = listOfIngredients;
        
        _dbContext.Ingredients.AddRange(listOfIngredients);
    await    _dbContext.SaveChangesAsync();
     await   SendAsync(Response, cancellation: ct);

    }
}

public class TestResponse
{
    public  List<Ingredient> Ingredients { get; set; }
}