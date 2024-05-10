using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Shared.Dtos;
using Shared.Model;
using UserTimelineService.Config;
using UserTimelineService.Context;

namespace UserTimelineService.Repository
{
    public class MySqlUPLRepository : IUPLRepository
    {
        private MySqlConnection _connection;
        private string _connectionString;

        public MySqlUPLRepository(MySqlConnection connection, IOptions<ConnectionStrings> connectionStrings)
        {
            _connection = connection;
            _connectionString = connectionStrings.Value.Default;
        }

        public async Task AddUPL(User_Post up)
        {
            await _connection.OpenAsync();
            await using var context = new PostContext(_connectionString);//!
            await context.user_posts.AddAsync(up);
            await context.SaveChangesAsync();
            await _connection.CloseAsync();
        }



        public async Task RemoveUPL(User_Post up)
        {
            await _connection.OpenAsync();

            await using var context = new UPLContext(_connectionString);//!

            var toRemove = context.user_posts.Where(el => el.PostId == up.PostId).ToList();
            context.user_posts.RemoveRange(toRemove);

            await context.SaveChangesAsync();

            await _connection.CloseAsync();
        }

        public async Task<IReadOnlyCollection<User_Post>> GetUPL()
        {
            await _connection.OpenAsync();

            await using var context = new PostContext(_connectionString);

            var comments = context.user_posts;

            await _connection.CloseAsync();

            return comments.ToList();
        }

    }
}