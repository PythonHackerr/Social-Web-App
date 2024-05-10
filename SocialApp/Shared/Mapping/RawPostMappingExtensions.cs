using Shared.Dtos;
using Shared.Model;

namespace Shared.Mapping;

public static class RawPostMappingExtensions
{
    public static RawPostDto ToRawDto(this Post post)
    {
        return new RawPostDto
        {
            Id = post.Id,
            AuthorId = post.Author_Id,
            Header = post.Header,
            Content = post.Content,
            Date = post.Date,
            likes = post.likes,
        };
    }

    public static Post ToModel(this RawPostDto post)
    {
        return new Post
        {
            Id = post.Id,
            Author_Id = post.AuthorId,
            Header = post.Header,
            Content = post.Content,
            Date = post.Date,
            likes = post.likes,
        };
    }
}