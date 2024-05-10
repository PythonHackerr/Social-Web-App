using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Shared.Dtos;
using Shared.Model;
using UserTimelineService.Config;
using UserTimelineService.Context;

namespace UserTimelineService.Repository
{
    public class MySqlUCLRepository : IUCLRepository
    {
        private MySqlConnection _connection;
        private string _connectionString;

        public MySqlUCLRepository(MySqlConnection connection, IOptions<ConnectionStrings> connectionStrings)
        {
            _connection = connection;
            _connectionString = connectionStrings.Value.Default;
        }

        public async Task AddUCL(User_Comment uc)
        {
            Console.WriteLine("+==============+__________1");
            await _connection.OpenAsync();



            // await using var context2 = new CommentContext(_connectionString);
            // Comment comment = new Comment
            // {
            //     PostId = 1,
            //     Id = 1,
            //     Author = null,
            //     Content = "a",
            //     Date = null,
            //     likes = 0,
            //     AuthorName = "as",
            // };
            // await context2.Comments.AddAsync(comment);




            Console.WriteLine("+==============+__________2");
            await using var context = new CommentContext(_connectionString);
            Console.WriteLine("+==============+__________3");
            await context.user_comments.AddAsync(uc);
            Console.WriteLine("+==============+__________4");
            await context.SaveChangesAsync();

            await _connection.CloseAsync();
        }



        public async Task RemoveUCL(User_Comment uc)
        {
            await _connection.OpenAsync();

            await using var context = new UCLContext(_connectionString);

            var toRemove = context.user_comments.Where(el => el.CommentId == uc.CommentId).ToList();
            context.user_comments.RemoveRange(toRemove);

            await context.SaveChangesAsync();

            await _connection.CloseAsync();
        }

        public async Task<IReadOnlyCollection<User_Comment>> GetUCL()
        {
            await _connection.OpenAsync();

            await using var context = new CommentContext(_connectionString);

            var comments = context.user_comments;

            await _connection.CloseAsync();

            return comments.ToList();
        }

    }
}