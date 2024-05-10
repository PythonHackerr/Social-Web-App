using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Shared.Exceptions;
using Shared.Model;
using UserProfileService.Config;
using UserProfileService.Context;

namespace UserProfileService.Repository
{
    public class MySqlUserRepository : IUserRepository
    {
        private MySqlConnection _connection;
        private string _connectionString;

        public MySqlUserRepository(MySqlConnection connection, IOptions<ConnectionStrings> connectionStrings)
        {
            _connection = connection;
            _connectionString = connectionStrings.Value.Default;
        }
        
        public MySqlUserRepository(MySqlConnection connection, string connectionString)
        {
            _connection = connection;
            _connectionString = connectionString;
        }

        public async Task<User?> GetUser(string userHandle)
        {
            await EnsureConnectionOpen();
            
            await using var context = new UserContext(_connectionString);
            
            // should probably use FindAsync() instead
            // but had some problems with it
            var user = context.Users.FirstOrDefault(user => user.Handle == userHandle);

            if (user == null)
            {
                throw new NotFoundException($"User with handle '{userHandle}'");
            }
            
            await _connection.CloseAsync();
            
            return user;
        }
        
        public async Task<User?> GetUserWithSub(string sub)
        {
            await EnsureConnectionOpen();
            
            await using var context = new UserContext(_connectionString);
            
            var user = context.Users.FirstOrDefault(user => user.Sub != null && user.Sub == sub);
            
            if (user == null)
            {
                throw new NotFoundException($"User with sub '{sub}'");
            }

            await _connection.CloseAsync();
                
            return user;
        }
        
        public async Task AddUser(User user)
        {
            await EnsureConnectionOpen();
            
            await using var context = new UserContext(_connectionString);
            
            if (await context.Users.AnyAsync(u => u.Handle == user.Handle))
            {
                throw new AlreadyExistsException($"User with handle '{user.Handle}'");
            }

            var newUser = new User()
            {
                Handle = user.Handle,
                Email = user.Email,
                DisplayName = user.DisplayName,
                Sub = user.Sub,
                JoinDate = DateTime.Today,
                FollowerCount = user.FollowerCount ?? 0,
                FollowingCount = user.FollowingCount ?? 0
            };
            
            await context.Users.AddAsync(newUser);

            await context.SaveChangesAsync();
            
            await _connection.CloseAsync();
        }
        
        public async Task DeleteUser(string handle)
        {
            await EnsureConnectionOpen();
            
            await using var context = new UserContext(_connectionString);
            
            if (await context.Users.AllAsync(u => u.Handle != handle))
            {
                throw new NotFoundException($"User with handle '{handle}'");
            }

            var toRemove = await context.Users.Where(user => user.Handle == handle).ToListAsync();

            context.RemoveRange(toRemove);
            
            await context.SaveChangesAsync();
            
            await _connection.CloseAsync();
        }

        public async Task<bool> HasProfileWithSub(string sub)
        {
            await EnsureConnectionOpen();
            
            await using var context = new UserContext(_connectionString);

            var found = await context.Users.AnyAsync(user => user.Sub == sub);

            await _connection.CloseAsync();

            return found;
        }

        public async Task<bool> HasProfileWithHandle(string handle)
        {
            await EnsureConnectionOpen();
            
            await using var context = new UserContext(_connectionString);

            var found = await context.Users.AnyAsync(user => user.Handle == handle);

            await _connection.CloseAsync();

            return found;
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