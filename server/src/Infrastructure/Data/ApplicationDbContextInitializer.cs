
using Timelogger.Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Timelogger.Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initializer = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitializer>();

        // await initializer.InitialiseAsync();

        await initializer.SeedAsync();
    }
}

public class ApplicationDbContextInitializer(
    ILogger<ApplicationDbContextInitializer> logger,
    ApplicationDbContext context)
{
    // public async Task InitialiseAsync()
    // {
    //     try
    //     {
    //         await context.Database.MigrateAsync();
    //     }
    //     catch (Exception ex)
    //     {
    //         logger.LogError(ex, "An error occurred while initialising the database.");
    //         throw;
    //     }
    // }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default data
        // Seed, if necessary
        if (!context.Projects.Any())
        {
            context.Projects.Add(new Project
            {
                Name = "My first Project",
                Deadline = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(2)),
                TimeRegistrations = new List<TimeRegistration>
                {
                    new TimeRegistration
                    {
                        Description = "Initial time registration",
                        Duration = 0.5,
                    }
                },
                IsCompleted = false,
            });

            await context.SaveChangesAsync();
        }
    }
}
