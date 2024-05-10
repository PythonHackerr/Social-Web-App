using FollowService.Config;
using FollowService.Context;
using Microsoft.Extensions.Options;
using MySqlConnector;
using Shared.Dtos;
using Shared.Model;

namespace FollowService.Repository
{
    public class MySqlFollowRepository : IFollowRepository
    {
        private MySqlConnection _connection;
        private string _connectionString;

        public MySqlFollowRepository(MySqlConnection connection, IOptions<ConnectionStrings> connectionStrings)
        {
            _connection = connection;
            _connectionString = connectionStrings.Value.Default;
        }

        public async Task<IReadOnlyCollection<User?>> GetFollowers(string userHandle)
        {
            await _connection.OpenAsync();
            await using var context = new FollowContext(_connectionString);

            var followers = context.Follows
                .Where(follow => follow.FollowingUser != null && follow.FollowingUser.Handle == userHandle)
                .Select(follow => follow.FollowerUser)
                .ToList();

            await _connection.CloseAsync();
            return followers;
        }
        
        public async Task<IReadOnlyCollection<User?>> GetFollowing(string userHandle)
        {
            await _connection.OpenAsync();
            await using var context = new FollowContext(_connectionString);

            var followers = context.Follows
                .Where(follow => follow.FollowerUser != null && follow.FollowerUser.Handle == userHandle)
                .Select(follow => follow.FollowingUser)
                .ToList();

            await _connection.CloseAsync();
            return followers;
        }
        
        public async Task<bool> IsFollowing(string followerHandle, string followingHandle)
        {
            await _connection.OpenAsync();
            await using var context = new FollowContext(_connectionString);

            var isFollowing = context.Follows.Any(follow => follow.FollowerUser != null 
                                                            && follow.FollowingUser != null
                                                            && follow.FollowerUser.Handle == followerHandle
                                                            && follow.FollowingUser.Handle == followingHandle);

            await _connection.CloseAsync();
            return isFollowing;
        }
        
        public async Task AddFollow(FollowDto follow)
        {
            await _connection.OpenAsync();
            await using var context = new FollowContext(_connectionString);

            if (!TryGetUsers(context, follow, out var follower, out var following))
            {
                return;
            }

            var followerId = follower?.Id;
            var followingId = following?.Id;

            var existingFollow = context.Follows.FirstOrDefault(f => f.Follower == followerId 
                                                                     && f.Following == followingId);

            if (existingFollow == null)
            {
                var newFollow = new Follow
                {
                    FollowerUser = follower,
                    FollowingUser = following
                };

                context.Follows.Add(newFollow);

                follower.FollowingCount++;
                following.FollowerCount++;

                context.Users.Update(follower);
                context.Users.Update(following);
            
                await context.SaveChangesAsync();
            }

            await _connection.CloseAsync();
        }

        public async Task RemoveFollow(FollowDto follow)
        {
            await _connection.OpenAsync();
            await using var context = new FollowContext(_connectionString);

            if (!TryGetUsers(context, follow, out var follower, out var following))
            {
                return;
            }

            var followerId = follower?.Id;
            var followingId = following?.Id;

            var existingFollow = context.Follows.FirstOrDefault(f => f.Follower == followerId 
                                                                     && f.Following == followingId);

            if (existingFollow is not null)
            {
                context.Follows.Remove(existingFollow);
                
                follower.FollowingCount--;
                following.FollowerCount--;

                context.Users.Update(follower);
                context.Users.Update(following);
                
                await context.SaveChangesAsync();
            }

            await _connection.CloseAsync();
        }

        private static bool TryGetUsers(FollowContext context, FollowDto follow,
            out User? follower, out User? following)
        {
            follower = context.Users.FirstOrDefault(user => user.Handle == follow.Follower);
            following = context.Users.FirstOrDefault(user => user.Handle == follow.Following);

            if (follower == null)
            {
                Console.WriteLine("Follower is null!");
                return false;
            }

            if (following == null)
            {
                Console.WriteLine("Following is null!");
                return false;
            }

            return true;
        }
    }
}