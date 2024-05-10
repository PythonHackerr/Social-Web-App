using Shared.Model;

namespace UserProfileService.Repository
{
    public interface IUserRepository
    {
        public Task<User?> GetUser(string userHandle);
        public Task<User?> GetUserWithSub(string sub);
        public Task AddUser(User user);
        public Task DeleteUser(string handle);
        public Task<bool> HasProfileWithSub(string sub);
        public Task<bool> HasProfileWithHandle(string handle);
    }
}