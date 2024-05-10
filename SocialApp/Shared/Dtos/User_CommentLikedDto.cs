using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public class User_CommentLikedDto
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("user_id")] public int UserId { get; set; }
        [JsonPropertyName("comment_id")] public int CommentId { get; set; }
    }
}