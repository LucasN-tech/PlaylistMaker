using PlaylistMaker.Domain.Entities;
using PlaylistMaker.Domain.Interfaces;
using PlaylistMaker.Infrastructure.Services;
using PlaylistMaker.Application.Services;
using PlaylistMaker.API.Swagger;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddUserSecrets<Program>()
    .AddEnvironmentVariables();

builder.Services.Configure<SpotifyAuth>(
    builder.Configuration.GetSection("Spotify"));

builder.Services.Configure<AzureVisionSettings>(
    builder.Configuration.GetSection("AzureVision"));

builder.Services.AddScoped<ISpotifyAuthService, SpotifyAuthService>();
builder.Services.AddScoped<IImageAnalyzer, AzureImageAnalyzer>();
builder.Services.AddScoped<ImageAnalyzerService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<AddFileUploadOperationFilter>();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();