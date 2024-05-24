global using FastEndpoints;
using Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API.Properties.Services;
using API.Services;
using Database.Models;
using FastEndpoints.Swagger;

var config = new ConfigurationBuilder()
    .AddUserSecrets<Program>()
    .Build();

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

// Add services to the container.
// Add application services
builder.Services.AddScoped<IWillysService, WillysService>();
builder.Services.AddScoped<IIcaService, IcaService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IIngredientService, IngredientService>();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSqlServer<WebApiDbContext>(config.GetConnectionString("azure"));

builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<WebApiDbContext>();

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(); // Add Swagger

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration["Jwt:Issuer"],
            ValidAudience = configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
        };
    });

builder.Services.AddAuthorization();

var app = builder.Build();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<User>();
app.UseFastEndpoints();
app.UseSwaggerGen();

/*await app.Services
    .CreateScope().ServiceProvider.GetRequiredService<WebApiDbContext>()
    .Database
    .EnsureCreatedAsync();
*/
app.Run();
