using Shared.Model;

namespace UserTimelineService.Repository
{
    public interface IUCLRepository
    {
        public Task AddUCL(User_Comment uc);
        public Task RemoveUCL(User_Comment uc);
        public Task<IReadOnlyCollection<User_Comment>> GetUCL();
    }
}