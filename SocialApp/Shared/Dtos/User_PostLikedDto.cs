using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public class User_PostLikedDto
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("user_id")] public int UserId { get; set; }
        [JsonPropertyName("post_id")] public int PostId { get; set; }
    }
}