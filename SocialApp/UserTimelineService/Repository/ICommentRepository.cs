using Shared.Model;

namespace UserTimelineService.Repository
{
    public interface ICommentRepository
    {
        public Task<IReadOnlyCollection<Comment>> GetComments();
        public Task<IReadOnlyCollection<Comment>> GetComments(int postId);
        public Task AddComment(Comment comment);
        public Task DeleteComment(int commentId);
        public Task AddLike(Comment comment);
        public Task RemoveLike(Comment comment);
    }
}