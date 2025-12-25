using System.Text.Json;
using Backend.Infrastructure.Extensions;
using Backend.WebApi.Middlewares;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition =
        System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddOpenApi();
builder.Services.AddInfrastructure(configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy("Custom", policy =>
    {
        policy.WithOrigins("http://localhost:5173");
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseCors("Custom");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();