// using Shared.Model;
//
// namespace UserTimelineService.Repository
// {
//     public class MockPostRepository : IPostRepository
//     {
//         public async Task<IReadOnlyCollection<Post>> GetPosts(string userHandle)
//         {
//             return Enumerable.Range(1, 5).Select(number => new Post()
//             {
//                 Author = null,
//                 Content = new string(number.ToString().FirstOrDefault(), number),
//                 Date = DateTime.Now + number * TimeSpan.FromDays(1)
//             }).ToList();
//         }
//     }
// }