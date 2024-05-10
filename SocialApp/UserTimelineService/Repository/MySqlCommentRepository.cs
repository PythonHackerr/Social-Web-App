using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Shared.Dtos;
using Shared.Model;
using UserTimelineService.Config;
using UserTimelineService.Context;

namespace UserTimelineService.Repository
{
    public class MySqlCommentRepository : ICommentRepository
    {
        private MySqlConnection _connection;
        private string _connectionString;

        public MySqlCommentRepository(MySqlConnection connection, IOptions<ConnectionStrings> connectionStrings)
        {
            _connection = connection;
            _connectionString = connectionStrings.Value.Default;
        }
        
        public MySqlCommentRepository(MySqlConnection connection, string connectionString)
        {
            _connection = connection;
            _connectionString = connectionString;
        }

        public async Task<IReadOnlyCollection<Comment>> GetComments()
        {
            await EnsureConnectionOpen();

            await using var context = new CommentContext(_connectionString);

            var comments = context.Comments;

            await _connection.CloseAsync();

            return comments.ToList();
        }
        
        public async Task<IReadOnlyCollection<Comment>> GetComments(int postId)
        {
            await EnsureConnectionOpen();

            await using var context = new CommentContext(_connectionString);

            var comments = context.Comments
                .Where(comment => comment.PostId == postId);

            await _connection.CloseAsync();

            return comments.ToList();
        }

        public async Task AddComment(Comment comment)
        {
            await EnsureConnectionOpen();

            await using var context = new CommentContext(_connectionString);
            await context.Comments.AddAsync(comment);
            await context.SaveChangesAsync();

            await _connection.CloseAsync();
        }
        
        public async Task DeleteComment(int commentId)
        {
            await EnsureConnectionOpen();

            await using var context = new CommentContext(_connectionString);

            var toDelete = context.Comments.Where(comment => comment.Id == commentId);
            
            context.Comments.RemoveRange(toDelete);
            
            await context.SaveChangesAsync();

            await _connection.CloseAsync();
        }

        public async Task AddLike(Comment comment)
        {
            await EnsureConnectionOpen();

            await using var context = new CommentContext(_connectionString);

            Comment c = context.Comments.FirstOrDefault(i => i.Id == comment.Id);
            c.likes += 1;
            comment.likes += 1;

            await context.SaveChangesAsync();

            await _connection.CloseAsync();
        }

        public async Task RemoveLike(Comment comment)
        {
            await _connection.OpenAsync();

            await using var context = new CommentContext(_connectionString);

            Comment c = context.Comments.FirstOrDefault(i => i.Id == comment.Id);
            c.likes -= 1;
            comment.likes -= 1;

            await context.SaveChangesAsync();

            await _connection.CloseAsync();
        }


        // public async Task<IReadOnlyCollection<Comment>> GetHomeTimelineForUserComment(int skip, int take)
        // {
        //     await _connection.OpenAsync();
        //
        //     await using var context = new CommentContext(_connectionString);
        //
        //     var comments = context.Comments
        //         //.Include(comment => comment.Author)
        //         //.Where(comment => following.Contains(comment.Author_Id))
        //         .OrderByDescending(comment => comment.Date)
        //         .Skip(skip)
        //         .Take(take);
        //
        //     await _connection.CloseAsync();
        //
        //     return comments.ToList();
        // }
        
        private async Task EnsureConnectionOpen()
        {
            try
            {
                await _connection.OpenAsync();
            }
            catch (InvalidOperationException)
            {
                // already open
            }
        }
    }
}