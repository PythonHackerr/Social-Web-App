using Shared.Dtos;
using Shared.Model;

namespace Shared.Mapping;

public static class User_CommentLikedMappingExtensions
{
    public static User_CommentLikedDto ToDto(this User_Comment uc)
    {
        return new User_CommentLikedDto
        {
            Id = uc.Id,
            UserId = uc.UserId,
            CommentId = uc.CommentId
        };
    }

    public static User_Comment ToModel(this User_CommentLikedDto uc)
    {
        return new User_Comment
        {
            Id = uc.Id,
            UserId = uc.UserId,
            CommentId = uc.CommentId
        };
    }
}