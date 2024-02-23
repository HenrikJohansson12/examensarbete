global using FastEndpoints;
using API.Properties.Services;
using FastEndpoints.Swagger; //add this

var builder = WebApplication.CreateBuilder();
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(); //add this
builder.Services.AddScoped<IWillysService,WillysService>();
builder.Services.AddSingleton<HttpClient>();

var app = builder.Build();
app.UseFastEndpoints();
app.UseSwaggerGen(); //add this
app.Run();