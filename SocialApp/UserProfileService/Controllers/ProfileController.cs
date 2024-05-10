using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Shared.Dtos;
using Shared.Exceptions;
using Shared.Mapping;
using UserProfileService.Repository;
using Route = Shared.Constant.Route;

namespace UserProfileService.Controllers
{
    [ApiController]
    [Route(Route.Profile)]
    [EnableCors]
    public class ProfileController : ControllerBase
    {
        private readonly IUserRepository _repository;

        public ProfileController(IUserRepository repository)
        {
            _repository = repository;
        }

        [HttpGet] [EnableCors] [Route(Route.GetProfileWithHandle)]
        public async Task<ActionResult<UserDto?>> GetProfile(string user)
        {
            try
            {
                return (await _repository.GetUser(user))?.ToDto();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }
        
        [HttpGet] [EnableCors] [Route(Route.GetProfileWithSub)]
        public async Task<ActionResult<UserDto?>> GetProfileWithSub(string sub)
        {
            try
            {
                return (await _repository.GetUserWithSub(sub))?.ToDto();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpPost] [EnableCors] [Route(Route.CreateProfile)]
        public async Task<IActionResult> CreateProfile(UserDto user)
        {
            try
            {
                await _repository.AddUser(user.ToModel());
                return Ok();
            }
            catch (AlreadyExistsException e)
            {
                return Conflict(e.Message);
            }
        }

        [HttpGet] [EnableCors] [Route(Route.HasProfileWithSub)]
        public bool IsSubRegistered(string sub) => _repository.HasProfileWithSub(sub).Result;

        [HttpGet] [EnableCors] [Route(Route.HasProfileWithHandle)]
        public bool IsHandleRegistered(string handle) => _repository.HasProfileWithHandle(handle).Result;
    }
}