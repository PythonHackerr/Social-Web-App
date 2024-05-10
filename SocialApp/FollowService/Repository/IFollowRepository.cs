using Shared.Dtos;
using Shared.Model;

namespace FollowService.Repository
{
    public interface IFollowRepository
    {
        public Task<IReadOnlyCollection<User?>> GetFollowers(string userHandle);
        public Task<IReadOnlyCollection<User?>> GetFollowing(string userHandle);
        public Task AddFollow(FollowDto follow);
        public Task RemoveFollow(FollowDto follow);
        public Task<bool> IsFollowing(string followerHandle, string followingHandle);
    }
}