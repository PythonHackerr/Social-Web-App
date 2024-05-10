using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Mapping;
using UserTimelineService.Repository;
using UserTimelineService.Tests;
using Route = Shared.Constant.Route;

namespace UserTimelineService.Controllers
{
    [ApiController]
    [EnableCors]
    [Route(Route.Timeline)]
    public class TimelineController : ControllerBase
    {
        private readonly ILogger<TimelineController> _logger;
        private readonly IPostRepository _repository;
        private readonly ICommentRepository _repositoryComment;
        private readonly IUPLRepository _repositoryUPL;
        private readonly IUCLRepository _repositoryUCL;

        public TimelineController(IPostRepository repository, ILogger<TimelineController> logger, ICommentRepository repositoryComment, IUCLRepository repositoryUCL, IUPLRepository repositoryUPL)
        {
            _logger = logger;
            _repository = repository;
            _repositoryComment = repositoryComment;
            _repositoryUCL = repositoryUCL;
            _repositoryUPL = repositoryUPL;
        }

        [HttpGet]
        [EnableCors]
        [Route(Route.GetPostsForUserHandle)]
        public IEnumerable<PostDto> GetPosts(string user) => _repository.GetPosts(user).Result.Select(post => post.ToDto());

        [HttpGet]
        [EnableCors]
        [Route(Route.GetComments)]
        public IEnumerable<CommentDto> GetComments() => _repositoryComment.GetComments().Result.Select(comment => comment.ToDto());

        [HttpGet]
        [EnableCors]
        [Route(Route.GetUCL)]
        public IEnumerable<User_CommentLikedDto> GetUCL() => _repositoryUCL.GetUCL().Result.Select(ucl => ucl.ToDto());

        [HttpGet]
        [EnableCors]
        [Route(Route.GetUPL)]
        public IEnumerable<User_PostLikedDto> GetUPL() => _repositoryUPL.GetUPL().Result.Select(upl => upl.ToDto());

        [HttpPost]
        [EnableCors]
        [Route(Route.AddPost)]
        public void AddPost(RawPostDto post) => _repository.AddPost(post.ToModel());

        [HttpPost]
        [EnableCors]
        [Route(Route.AddComment)]
        public void AddComment(RawCommentDto comment) => _repositoryComment.AddComment(comment.ToModel());

        [HttpPost]
        [EnableCors]
        [Route(Route.AddLike)]
        public void AddLike(RawCommentDto comment) => _repositoryComment.AddLike(comment.ToModel());

        [HttpPost]
        [EnableCors]
        [Route(Route.RemoveLike)]
        public void RemoveLike(RawCommentDto comment) => _repositoryComment.RemoveLike(comment.ToModel());

        [HttpPost]
        [EnableCors]
        [Route(Route.AddUCL)]
        public void AddUCL(User_CommentLikedDto ucl) => _repositoryUCL.AddUCL(ucl.ToModel());

        [HttpPost]
        [EnableCors]
        [Route(Route.RemoveUCL)]
        public void RemoveUCL(User_CommentLikedDto ucl) => _repositoryUCL.RemoveUCL(ucl.ToModel());


        [HttpPost]
        [EnableCors]
        [Route(Route.AddLikePost)]
        public void AddLikePost(RawPostDto post) => _repository.AddLikePost(post.ToModel());

        [HttpPost]
        [EnableCors]
        [Route(Route.RemoveLikePost)]
        public void RemoveLikePost(RawPostDto post) => _repository.RemoveLikePost(post.ToModel());

        [HttpPost]
        [EnableCors]
        [Route(Route.AddUPL)]
        public void AddUPL(User_PostLikedDto upl) => _repositoryUPL.AddUPL(upl.ToModel());

        [HttpPost]
        [EnableCors]
        [Route(Route.RemoveUPL)]
        public void RemoveUPL(User_PostLikedDto upl) => _repositoryUPL.RemoveUPL(upl.ToModel());




        [HttpGet]
        [EnableCors]
        [Route(Route.GetHomeTimeline)]
        public IEnumerable<PostDto> GetHomeTimelineForUser(string user, int skip, int take)
            => _repository.GetHomeTimelineForUser(user, skip, take).Result.Select(post => post.ToDto());

        // not sure what it was supposed to do
        // [HttpGet]
        // [EnableCors]
        // [Route(Route.GetHomeTimeline2)]
        // public IEnumerable<CommentDto> GetHomeTimelineForUserComment(int skip, int take)
        //             => _repositoryComment.GetHomeTimelineForUserComment(skip, take).Result.Select(comment => comment.ToDto());

        [Obsolete]
        [HttpGet]
        [Route("RunTests")]
        public string RunTests(string host = "localhost", uint port = 44355)
        {
            var test = new EndpointTest(host, port, _repository);

            return $"{test.TestGetTweets()}";
        }
    }
}