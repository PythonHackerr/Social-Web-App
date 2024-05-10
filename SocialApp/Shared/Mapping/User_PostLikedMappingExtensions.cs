using Shared.Dtos;
using Shared.Model;

namespace Shared.Mapping;

public static class User_PostLikedMappingExtensions
{
    public static User_PostLikedDto ToDto(this User_Post uc)
    {
        return new User_PostLikedDto
        {
            Id = uc.Id,
            UserId = uc.UserId,
            PostId = uc.PostId
        };
    }

    public static User_Post ToModel(this User_PostLikedDto uc)
    {
        return new User_Post
        {
            Id = uc.Id,
            UserId = uc.UserId,
            PostId = uc.PostId
        };
    }
}