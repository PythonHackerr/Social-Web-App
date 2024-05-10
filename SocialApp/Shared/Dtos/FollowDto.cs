using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public class FollowDto
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("follower")] public string? Follower { get; set; }
        [JsonPropertyName("following")] public string? Following { get; set; }
    }
}