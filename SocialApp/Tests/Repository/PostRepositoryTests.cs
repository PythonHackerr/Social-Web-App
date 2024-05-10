using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using MySqlConnector;
using Shared.Exceptions;
using Shared.Model;
using Tests.Helpers;
using UserProfileService.Repository;
using UserTimelineService.Repository;
using Xunit;

namespace Tests.Repository;

public class PostRepositoryTests : IAsyncLifetime
{
    private IPostRepository Repo { get; set; }

    private User User { get; set; }
    private Post Post { get; set; }
    private string Handle { get; set; }
    private int? PostId { get; set; }
    
    public async Task InitializeAsync()
    {
        Repo = new MySqlPostRepository(DbHelper.Connection(), DbHelper.ConnectionString);

        var random = new Random();

        Handle = Guid.NewGuid().ToString();
        
        User = new User
        {
            Handle = Handle,
            DisplayName = Guid.NewGuid().ToString(),
            Email = Guid.NewGuid().ToString(),
            FollowerCount = random.Next(),
            FollowingCount = random.Next(),
            JoinDate = DateTime.Today,
            Sub = Guid.NewGuid().ToString()
        };
        
        Post = new Post
        {
            Author_Id = random.Next(),
            Header = Guid.NewGuid().ToString(),
            Content = Guid.NewGuid().ToString(),
            Date = DateTime.Today + TimeSpan.FromHours(13) + TimeSpan.FromMinutes(13),
            Author = User
        };
    }

    public async Task DisposeAsync()
    {
        try
        {
            if (PostId is { } notNullId) await Repo.DeletePost(notNullId);
        }
        catch (NotFoundException)
        {
            // nothing to clean up
        }
    }

    [Fact]
    public async Task AddPost()
    {
        await Repo.AddPost(Post);
        PostId = Post.Id;
        var posts = await Repo.GetPosts(Handle);
        var latestPost = posts.FirstOrDefault();
        
        Assert.NotNull(latestPost);
        
        Assert.True(posts.Count > 0);
        
        Assert.True(latestPost.Id >= 0);

        Post.Author = null;
        Assert.Equal(Post, latestPost);
    }
}