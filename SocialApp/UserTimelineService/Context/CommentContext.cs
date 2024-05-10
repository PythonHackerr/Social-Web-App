using Microsoft.EntityFrameworkCore;
using Shared.Model;

namespace UserTimelineService.Context;

public class CommentContext : DbContext
{
    private string _connection;

    public DbSet<Comment> Comments { get; set; }
    public DbSet<User_Comment> user_comments { get; set; }

    public CommentContext(string connectionString)
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