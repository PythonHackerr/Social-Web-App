using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public class PostDto
    {
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("author_id")] public UserDto? Author { get; set; }
        [JsonPropertyName("header")] public string? Header { get; set; }
        [JsonPropertyName("content")] public string? Content { get; set; }
        [JsonPropertyName("likes")] public int likes { get; set; }
        [JsonPropertyName("creation_time")] public DateTime? Date { get; set; }
    }
}