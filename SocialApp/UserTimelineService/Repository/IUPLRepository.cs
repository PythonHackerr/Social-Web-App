using Shared.Model;

namespace UserTimelineService.Repository
{
    public interface IUPLRepository
    {
        public Task AddUPL(User_Post up);
        public Task RemoveUPL(User_Post up);
        public Task<IReadOnlyCollection<User_Post>> GetUPL();
    }
}