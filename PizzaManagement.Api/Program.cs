using Microsoft.EntityFrameworkCore;

using PizzaManagement.Api;
using PizzaManagement.Application;
using PizzaManagement.Infrastructure;
using PizzaManagement.Infrastructure.Persistance;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration, builder.Environment);
}

var app = builder.Build();
{
    app.UseCors("CorsPolicy");
    app.UseStaticFiles();
    app.UseForwardedHeaders();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    app.MapFallbackToController("Index", "Fallback");

    if (app.Environment.IsDevelopment())
    {
        using var scope = app.Services.CreateScope();
        var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
        try
        {
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await context.Database.MigrateAsync();
            await ApplicationDbContextSeed.SeedAsync(context, loggerFactory);
        }
        catch (Exception ex)
        {
            var logger = loggerFactory.CreateLogger<Program>();
            logger.LogError(ex, "An error occured during migration");
        }
    }
    
    app.Run();
}

