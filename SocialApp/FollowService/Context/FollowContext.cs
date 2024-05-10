using Microsoft.EntityFrameworkCore;
using Shared.Model;

namespace FollowService.Context;

public class FollowContext : DbContext
{
    private string _connection;
    
    public DbSet<User> Users { get; set; }
    public DbSet<Follow> Follows { get; set; }

    public FollowContext(string connectionString)
    {
        _connection = connectionString;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseMySql(_connection, ServerVersion.AutoDetect(_connection))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();;
    }
}