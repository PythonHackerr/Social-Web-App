using Microsoft.EntityFrameworkCore;
using Shared.Model;

namespace UserTimelineService.Context;

public class UCLContext : DbContext
{
    private string _connection;

    public DbSet<User_Comment> user_comments { get; set; }

    public UCLContext(string connectionString)
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