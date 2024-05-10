using FollowService.Repository;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Mapping;
using Route = Shared.Constant.Route;

namespace FollowService.Controllers
{
    [ApiController]
    [Route(Route.Follow)]
    [EnableCors]
    public class FollowController : ControllerBase
    {
        private readonly IFollowRepository _repository;

        public FollowController(IFollowRepository repository)
        {
            _repository = repository;
        }

        [HttpGet] [EnableCors] [Route(Route.GetFollowers)]
        public IEnumerable<UserDto> GetFollowers(string user) => _repository.GetFollowers(user).Result
            .Select(user => user?.ToDto());
        
        [HttpGet] [EnableCors] [Route(Route.GetFollowing)]
        public IEnumerable<UserDto> GetFollowing(string user) => _repository.GetFollowing(user).Result
            .Select(user => user?.ToDto());
        
        [HttpPost] [EnableCors] [Route(Route.AddFollow)]
        public void Follow(FollowDto follow) => _repository.AddFollow(follow);
        
        [HttpPost] [EnableCors] [Route(Route.RemoveFollow)]
        public void Unfollow(FollowDto follow) => _repository.RemoveFollow(follow);
        
        [HttpGet] [EnableCors] [Route(Route.IsFollowing)]
        public bool IsFollowing(string follower, string following) => _repository.IsFollowing(follower, following).Result;
    }
}