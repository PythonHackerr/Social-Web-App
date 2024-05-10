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

public class CommentRepositoryTests : IAsyncLifetime
{
    private ICommentRepository Repo { get; set; }

    private Comment Comment { get; set; }
    private int PostId { get; set; }
    private int CommentId { get; set; }
    private int CommenterId { get; set; }
    
    public async Task InitializeAsync()
    {
        Repo = new MySqlCommentRepository(DbHelper.Connection(), DbHelper.ConnectionString);

        var random = new Random();

        CommenterId = random.Next();
        PostId = random.Next();

        Comment = new Comment
        {
            Author_Id = CommenterId,
            PostId = PostId,
            Content = Guid.NewGuid().ToString(),
            Date = DateTime.Now,
            likes = random.Next(),
        };
    }

    public async Task DisposeAsync()
    {
        try
        {
            if (CommentId is { } notNullId) await Repo.DeleteComment(notNullId);
        }
        catch (NotFoundException)
        {
            // nothing to clean up
        }
    }

    [Fact]
    public async Task AddComment()
    {
        await Repo.AddComment(Comment);
        CommentId = Comment.Id;

        var comments = await Repo.GetComments(PostId);
        var newComment = comments.FirstOrDefault(com => com.Id == CommentId);
        
        Assert.True(comments.Count > 0);
        Assert.NotNull(newComment);
    }
    
    [Fact]
    public async Task LikeComment()
    {
        await Repo.AddComment(Comment);
        CommentId = Comment.Id;

        var previousLikes = Comment.likes;

        await Repo.AddLike(Comment);

        var comments = await Repo.GetComments(PostId);
        var newComment = comments.FirstOrDefault(com => com.Id == CommentId);

        Assert.NotNull(newComment);
        
        var nextLikes = newComment.likes;
        
        Assert.True(nextLikes > previousLikes);
        Assert.True(nextLikes == previousLikes + 1);
    }
}