global using FastEndpoints;
using Database;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;
using API.Properties.Services;
using API.Services;
using FastEndpoints.Swagger;
using Google.Apis.Auth;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Pizzas") ?? "Data Source=C:\\dev\\Examensarbete\\Database\\WebApiDatabaseTest.db";
var configuration = builder.Configuration;

// Add services to the container.
// Add application services
builder.Services.AddScoped<IWillysService, WillysService>();
builder.Services.AddScoped<IIcaService, IcaService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddSqlite<WebApiDbContext>(connectionString);

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<WebApiDbContext>();

builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(); // Add Swagger

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

app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<IdentityUser>();
app.UseFastEndpoints();
app.UseSwaggerGen();

/*await app.Services
    .CreateScope().ServiceProvider.GetRequiredService<WebApiDbContext>()
    .Database
    .EnsureCreatedAsync();
*/
app.Run();




