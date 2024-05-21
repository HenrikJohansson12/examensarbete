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
        Get("/api/ingredients");
        AllowAnonymous();
    }

    public override async Task HandleAsync( CancellationToken ct)
    {
        Response.Ingredients = await _dbContext.Ingredients.ToListAsync();
     await   SendAsync(Response, cancellation: ct);
    }
}

public class TestResponse
{
    public  List<Ingredient> Ingredients { get; set; }
}