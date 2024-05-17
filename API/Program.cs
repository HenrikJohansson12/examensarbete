global using FastEndpoints;
using API.Properties.Services;
using API.Services;
using Database;
using FastEndpoints.Security;
using FastEndpoints.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;

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

builder.Services
    .AddAuthenticationJwtBearer(s => s.SigningKey = "A_Secret_Token_Signing_Key_Longer_Than_32_Characters");

//register identity/cookie auth scheme
/*    .AddEntityFrameworkStores<WebApiDbContext>()
    .AddSignInManager();*/
builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddEntityFrameworkStores<WebApiDbContext>();

//override the behavior or cookie auth scheme so that 401/403 will be returned.
builder.Services
    .ConfigureApplicationCookie(
        c =>
        {
            c.Events.OnRedirectToLogin
                = ctx =>
                {
                    if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        ctx.Response.StatusCode = 401;

                    return Task.CompletedTask;
                };
            c.Events.OnRedirectToAccessDenied
                = ctx =>
                {
                    if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
                        ctx.Response.StatusCode = 403;

                    return Task.CompletedTask;
                };
        });

//setting jwt auth scheme to be the default. jwt will be tried first to authenticate all incoming requests, unless otherwise specified at the endpoint level.
builder.Services.AddAuthentication(o => o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme);
builder.Services.AddAuthorization();


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<IdentityUser>();
// Configure middleware
app.UseCors("AllowSpecificOrigin");
app.UseFastEndpoints();
app.UseSwaggerGen(); // Add Swagger
await app.Services
    .CreateScope().ServiceProvider.GetRequiredService<WebApiDbContext>()
    .Database
    .EnsureCreatedAsync();

app.Run();