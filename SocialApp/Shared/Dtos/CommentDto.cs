using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public class CommentDto
    {
        [JsonPropertyName("post_id")] public int PostId { get; set; }
        [JsonPropertyName("id")] public int Id { get; set; }
        [JsonPropertyName("author_id")] public UserDto? Author { get; set; }
        [JsonPropertyName("content")] public string? Content { get; set; }
        [JsonPropertyName("creation_time")] public DateTime? Date { get; set; }
        [JsonPropertyName("likes")] public int likes { get; set; }
        [JsonPropertyName("author_name")] public string? AuthorName { get; set; }
    }
}