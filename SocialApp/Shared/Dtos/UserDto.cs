using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public class UserDto
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("handle")] public string? Handle { get; set; }
        [JsonPropertyName("email")] public string? Email { get; set; }
        [JsonPropertyName("join_date")] public DateTime? JoinDate { get; set; }
        [JsonPropertyName("display_name")] public string? DisplayName { get; set; }
        [JsonPropertyName("sub")] public string? Sub { get; set; }
        [JsonPropertyName("followers")] public int? FollowerCount { get; set; }
        [JsonPropertyName("following")] public int? FollowingCount { get; set; }
    }
}