global using FastEndpoints;
using API.Properties.Services;
using FastEndpoints.Swagger; //add this

var builder = WebApplication.CreateBuilder();
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(); //add this
builder.Services.AddScoped<IStoreServiceFactory, StoreServiceFactory>();
builder.Services.AddSingleton<IcaService>();
builder.Services.AddSingleton<WillysService>();
builder.Services.AddScoped<HttpClient>();


var app = builder.Build();
app.UseFastEndpoints();
app.UseSwaggerGen(); //add this
app.Run();