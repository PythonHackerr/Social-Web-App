using Microsoft.EntityFrameworkCore;
using Shared.Model;

namespace UserProfileService.Context;

public class UserContext : DbContext
{
    private string _connection;
    
    public DbSet<User> Users { get; set; }
    // public DbSet<Post> Posts { get; set; }

    public UserContext(string connectionString)
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