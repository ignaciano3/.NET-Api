using Application;
using Scalar.AspNetCore;
using Infrastructure;
using PruebaApi.Configuration;
using PruebaApi.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi("v1", options =>
    options.AddDocumentTransformer<BearerSecuritySchemeTransformer>());


builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/docs",options => options
        .WithDefaultHttpClient(ScalarTarget.Node, ScalarClient.Fetch)
        .WithTitle("API Reference Pruebas")
        .WithTheme(ScalarTheme.Solarized)
        .WithDocumentDownloadType(DocumentDownloadType.None)
    );
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapEndpoints();


app.Run();