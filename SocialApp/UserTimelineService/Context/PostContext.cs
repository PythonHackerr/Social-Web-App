using Microsoft.EntityFrameworkCore;
using Shared.Model;

namespace UserTimelineService.Context;

public class PostContext : DbContext
{
    private string _connection;

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<Follow> Follows { get; set; }
    public DbSet<User_Post> user_posts { get; set; }
    public PostContext(string connectionString)
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