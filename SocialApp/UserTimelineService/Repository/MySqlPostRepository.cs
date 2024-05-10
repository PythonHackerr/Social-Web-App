using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Shared.Dtos;
using Shared.Model;
using UserTimelineService.Config;
using UserTimelineService.Context;

namespace UserTimelineService.Repository
{
    public class MySqlPostRepository : IPostRepository
    {
        private MySqlConnection _connection;
        private string _connectionString;

        public MySqlPostRepository(MySqlConnection connection, IOptions<ConnectionStrings> connectionStrings)
        {
            _connection = connection;
            _connectionString = connectionStrings.Value.Default;
        }

        public MySqlPostRepository(MySqlConnection connection, string connectionString)
        {
            _connection = connection;
            _connectionString = connectionString;
        }

        public async Task<IReadOnlyCollection<Post>> GetPosts(string userHandle)
        {
            await _connection.OpenAsync();

            await using var context = new PostContext(_connectionString);

            var posts = context.Posts
                .Where(post => post.Author.Handle == userHandle)
                .OrderByDescending(post => post.Date);

            await _connection.CloseAsync();

            return posts.ToList();
        }

        public async Task AddPost(Post post)
        {
            await _connection.OpenAsync();

            await using var context = new PostContext(_connectionString);

            await context.Posts.AddAsync(post);
            // context.Entry(post.Author).State = EntityState.Unchanged;
            await context.SaveChangesAsync();

            await _connection.CloseAsync();
        }

        public async Task DeletePost(int id)
        {
            await EnsureConnectionOpen();

            await using var context = new PostContext(_connectionString);

            var toRemove = context.Posts.Where(post => post.Id == id).ToList();

            context.Posts.RemoveRange(toRemove);

            await context.SaveChangesAsync();

            await _connection.CloseAsync();
        }


        public async Task AddLikePost(Post post)
        {
            await EnsureConnectionOpen();

            await using var context = new PostContext(_connectionString);

            Post c = context.Posts.FirstOrDefault(i => i.Id == post.Id);
            c.likes += 1;
            post.likes += 1;

            await context.SaveChangesAsync();

            await _connection.CloseAsync();
        }

        public async Task RemoveLikePost(Post post)
        {
            await _connection.OpenAsync();

            await using var context = new PostContext(_connectionString);

            Post c = context.Posts.FirstOrDefault(i => i.Id == post.Id);
            c.likes -= 1;
            post.likes -= 1;

            await context.SaveChangesAsync();

            await _connection.CloseAsync();
        }


        public async Task<IReadOnlyCollection<Post>> GetHomeTimelineForUser(string userHandle, int skip, int take)
        {
            await EnsureConnectionOpen();

            await using var context = new PostContext(_connectionString);

            var following = context.Follows
                .Where(follow => follow.FollowerUser != null && follow.FollowerUser.Handle == userHandle)
                .Select(follow => follow.Following);

            var posts = context.Posts
                .Include(post => post.Author)
                .Where(post => following.Contains(post.Author_Id))
                .OrderByDescending(post => post.Date)
                .Skip(skip)
                .Take(take);

            await _connection.CloseAsync();

            return posts.ToList();
        }

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