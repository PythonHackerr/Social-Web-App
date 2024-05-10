using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Model
{
    [Table("users_likedposts")]
    public class User_Post
    {
        [Column("id")] public int Id { get; set; }
        [Column("user_id")] public int UserId { get; set; }
        [Column("post_id")] public int PostId { get; set; }
    }
}