global using FastEndpoints;
using API.Properties.Services;
using API.Services;
using Database;
using FastEndpoints.Swagger;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

var builder = WebApplication.CreateBuilder();
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=C:\\dev\\Examensarbete\\Database\\WebApiDatabaseTest.db";

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policyBuilder => policyBuilder.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// Configure FastEndpoints and Swagger
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(); // Add Swagger

// Add application services
builder.Services.AddScoped<IWillysService, WillysService>();
builder.Services.AddScoped<IIcaService, IcaService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSqlite<WebApiDbContext>(connectionString);

// Configure DbContext with SQLite

var app = builder.Build();



// Configure middleware
app.UseCors("AllowSpecificOrigin");
app.UseFastEndpoints();
app.UseSwaggerGen(); // Add Swagger

app.Run();