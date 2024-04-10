global using FastEndpoints;
using API.Properties.Services;
using API.Services;
using Database;
using FastEndpoints.Swagger; //add this

var builder = WebApplication.CreateBuilder();
builder.Services.AddFastEndpoints();
builder.Services.SwaggerDocument(); //add this
builder.Services.AddScoped<IWillysService,WillysService>();
builder.Services.AddScoped<IIcaService,IcaService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddSingleton<HttpClient>();
builder.Services.AddScoped<WebApiDbContext>();

var app = builder.Build();
app.UseFastEndpoints();
app.UseSwaggerGen(); //add this
app.Run();