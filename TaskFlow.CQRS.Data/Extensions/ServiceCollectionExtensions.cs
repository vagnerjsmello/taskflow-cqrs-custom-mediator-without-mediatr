using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaskFlow.CQRS.Data.Context;

namespace TaskFlow.CQRS.Data.Extensions;

/// <summary>
/// Extension methods for configuring persistence services.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the ApplicationDbContext using SQLite In-Memory.
    /// Keeps the database connection open for the lifetime of the application.
    /// </summary>
    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        // Create a single shared SQLite in-memory connection
        var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open(); // required to keep the database alive        

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connection));

        return services;
    }
}
