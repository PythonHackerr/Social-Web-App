using Microsoft.EntityFrameworkCore;
using Shared.Model;

namespace UserTimelineService.Context;

public class UPLContext : DbContext
{
    private string _connection;

    public DbSet<User_Post> user_posts { get; set; }

    public UPLContext(string connectionString)
    {
        _connection = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseMySql(_connection, ServerVersion.AutoDetect(_connection))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
    }
}