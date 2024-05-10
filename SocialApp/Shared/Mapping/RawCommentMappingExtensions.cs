using Shared.Dtos;
using Shared.Model;

namespace Shared.Mapping;

public static class RawCommentMappingExtensions
{
    public static RawCommentDto ToRawDto(this Comment comment)
    {
        return new RawCommentDto
        {
            PostId = comment.PostId,
            Id = comment.Id,
            AuthorId = comment.Author?.Id,
            Content = comment.Content,
            Date = comment.Date,
            likes = comment.likes,
            AuthorName = comment.AuthorName,
        };
    }

    public static Comment ToModel(this RawCommentDto comment)
    {
        return new Comment
        {
            PostId = comment.PostId,
            Id = comment.Id,
            Author_Id = comment.AuthorId,
            Content = comment.Content,
            Date = comment.Date,
            likes = comment.likes,
            AuthorName = comment.AuthorName,
        };
    }
}