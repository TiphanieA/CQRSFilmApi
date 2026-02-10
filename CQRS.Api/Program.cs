using CQRS.Api.Middleware;
using Microsoft.OpenApi.Models;
using CQRS.Api.Middleware;
using CQRS.Application.Extensions;
using CQRS.Infrastructure.Extensions;
using CQRS.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddSwaggerGen(swaggerGen =>
{
    swaggerGen.EnableAnnotations();
    swaggerGen.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "CQRS Films API",
        Description = "Api for manage films",
        Version = "v1"
    });
});

var app = builder.Build();

app.UseMiddleware<GlobalExceptionHandler>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(swagger =>
    {
        swagger.SwaggerEndpoint("/swagger/v1/swagger.json", "CQRS.Api");
    });
}

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

await app.RunAsync();