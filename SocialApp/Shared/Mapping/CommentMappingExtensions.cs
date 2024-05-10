using Shared.Dtos;
using Shared.Model;

namespace Shared.Mapping;

public static class CommentMappingExtensions
{
    public static CommentDto ToDto(this Comment comment)
    {
        return new CommentDto
        {
            PostId = comment.PostId,
            Id = comment.Id,
            Author = comment.Author?.ToDto(),
            Content = comment.Content,
            Date = comment.Date,
            likes = comment.likes,
            AuthorName = comment.AuthorName,
        };
    }

    public static Comment ToModel(this CommentDto comment)
    {
        return new Comment
        {
            PostId = comment.PostId,
            Id = comment.Id,
            Author = comment.Author?.ToModel(),
            Content = comment.Content,
            Date = comment.Date,
            likes = comment.likes,
            AuthorName = comment.AuthorName,
        };
    }
}